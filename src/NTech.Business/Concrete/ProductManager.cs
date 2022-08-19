using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entity.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.URI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Extensions;
using NTech.Business.Helpers;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Concrete
{
    public class ProductManager : AsyncBaseService<Product, ProductWriteDto, ProductReadDto>, IProductService
    {
        private readonly IUriService _uriService;
        private readonly ICacheManager _cacheManager;
        private readonly IImageService _imageService;
        private readonly IOfferDal _offerDal;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IUriService uriService, ICacheManager cacheManager, IImageService imageService, IOfferDal offerDal, IMessageBrokerHelper messageBrokerHelper) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _uriService = uriService;
            _cacheManager = cacheManager;
            _imageService = imageService;
            _offerDal = offerDal;
            _messageBrokerHelper = messageBrokerHelper;
        }

        [SecuredOperation("User")]
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        [CacheRemoveAspect("ProductReadDto")]
        public async override Task<IResult> AddAsync(ProductWriteDto dto)
        {
            var result = BusinessRule.Run(
                //await checkImageAsync(dto)
                );
            if (result != null)
                return result;
            return await base.AddAsync(dto);
        }

        private async Task<IResult> checkImageAsync(ProductWriteDto dto)
        {
            var image = await _imageService.GetByIdAsync((int)dto.ImageId);
            if (image != null)
                return new ErrorResult();
            return new SuccessResult();
        }

        [SecuredOperation("User")]
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> UpdateAsync(int id, ProductWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }

        public async Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetPaginationAsync(PaginationFilter paginationFilter, string route)
        {
            string key = $"product+{paginationFilter.PageNumber}+{paginationFilter.PageSize}";
            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<PaginatedResult<IEnumerable<ProductReadDto>>>(key);
            }
            List<ProductReadDto> products = Mapper.Map<List<ProductReadDto>>(Repository.GetAll(false).ToList());
            var result = PaginationHelper.CreatePaginatedResponse(products, paginationFilter, products.Count(), _uriService, route);

            _cacheManager.Add(key, result, 30);
            return result;
        }
        public async Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetListByFilterAsync(PaginationFilter paginationFilter, string route, ProductFilterResource productFilterResource)
        {
            IQueryable<Product> products = Repository.GetAll(false);
            products = setFilter(products, productFilterResource);

            List<ProductReadDto> productReadDtos = Mapper.Map<List<ProductReadDto>>(await products.ToListAsync());
            var result = PaginationHelper.CreatePaginatedResponse(productReadDtos, paginationFilter, productReadDtos.Count(), _uriService, route);
            return result;
        }

        private IQueryable<Product> setFilter(IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            if (productFilterResource.Name != null)
                products = products.getByName(productFilterResource);

            if (productFilterResource.Description != null)
                products = products.getByDescription(productFilterResource);

            if (productFilterResource.MaximumPrice != null)
                products = products.getByMaximumPrice(productFilterResource);

            if (productFilterResource.MinimumPrice != null)
                products = products.getByMinimumPrice(productFilterResource);

            if (productFilterResource.UsingStatusId != null)
                products = products.getByUsingStatusId(productFilterResource);

            if (productFilterResource.ColorId != null)
                products = products.getByColorId(productFilterResource);

            if (productFilterResource.CategoryId != null)
                products = products.getByCategoryId(productFilterResource);

            if (productFilterResource.BrandId != null)
                products = products.getByBrandId(productFilterResource);

            return products;
        }

        [SecuredOperation("User")]
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }

        [SecuredOperation("User")]
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }

        [SecuredOperation("User")]
        public async Task<IResult> BuyAsync(int productId)
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            Offer offer = await _offerDal.GetAsync(x => x.ProductId == productId && x.UserId == userId);
            Product product = await Repository.GetAsync(p => p.Id == productId);

            if (product == null)
                return new SuccessResult(LanguageMessage.FailedGet);

            product.IsSold = true;
            if (offer != null)
            {
                offer.Status = true;
            }
            deleteOtherOffers(offer);
            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.ProductBuyIsSuccessfully) :
                new ErrorResult(LanguageMessage.ProductBuyIsFailed);
        }
        private async Task deleteOtherOffers(Offer offer)
        {
            //get denied or waited offers
            var offers = await _offerDal.GetAll(o => o.ProductId == offer.ProductId && o.Id != offer.Id).ToListAsync();

            foreach (Offer o in offers)
            {
                sendEmail(o);
                await _offerDal.DeleteAsync(o);
            }
        }
        private void sendEmail(Offer offer)
        {
            _messageBrokerHelper.QueueMessage(QueueNameEnum.EmailQueue.ToString(),
                new EmailQueue
                {
                    Subject = "Teklif verdiğiniz ürün satıldı",
                    Body = $"Merhaba {offer.User.FirstName} {offer.User.LastName}, teklif verdiğiniz ürün satıldı :(",
                    Email = offer.User.Email
                });
        }

        [SecuredOperation("User")]
        public async Task<IResult> SetImageAsync(int productId, IFormFile file)
        {
            Product product = await Repository.GetAsync(p => p.Id == productId);
            if (product == null)
                return new ErrorResult(LanguageMessage.NotFound);

            var imageResult = await _imageService.UploadAsync(file);
            if (imageResult.Success == false)
                return imageResult;

            product.ImageId = imageResult.Data.Id;
            await Repository.UpdateAsync(product);
            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.SuccessfullyFileUpload) :
                new ErrorResult(LanguageMessage.FailedToFileUpload);
        }

    }
}
