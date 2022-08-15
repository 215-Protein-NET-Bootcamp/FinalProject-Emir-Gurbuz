using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.URI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Helpers;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    [SecuredOperation("User")]
    public class ProductManager : AsyncBaseService<Product, ProductWriteDto, ProductReadDto>, IProductService
    {
        private readonly IUriService _uriService;
        private readonly ICacheManager _cacheManager;
        private readonly IImageService _imageService;
        private readonly ILanguageMessage _languageMessage;
        private readonly IOfferDal _offerDal;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IUriService uriService, ICacheManager cacheManager, IImageService imageService, IOfferDal offerDal, IMessageBrokerHelper messageBrokerHelper) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _uriService = uriService;
            _cacheManager = cacheManager;
            _imageService = imageService;
            _languageMessage = languageMessage;
            _offerDal = offerDal;
            _messageBrokerHelper = messageBrokerHelper;
        }
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        [CacheRemoveAspect("ProductReadDto")]
        public async override Task<IResult> AddAsync(ProductWriteDto dto)
        {
            var result = BusinessRule.Run(
                await checkImageAsync(dto));
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

        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }

        public async Task<IResult> BuyAsync(int productId)
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            Offer offer = await _offerDal.GetAsync(x => x.ProductId == productId && x.UserId == userId);
            Product product = await Repository.GetAsync(p => p.Id == productId);

            if (product == null)
                return new SuccessResult(_languageMessage.FailedGet);

            product.IsSold = true;
            if (offer != null)
            {
                offer.Status = true;
            }
            deleteOtherOffers(offer);
            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(_languageMessage.ProductBuyIsSuccessfully) :
                new ErrorResult(_languageMessage.ProductBuyIsFailed);
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
            _messageBrokerHelper.QueueMessage(
                new EmailQueue
                {
                    Subject = "Teklif verdiğiniz ürün satıldı",
                    Body = $"Merhaba {offer.User.FirstName} {offer.User.LastName}, teklif verdiğiniz ürün satıldı :(",
                    Email = offer.User.Email
                });
        }
    }
}
