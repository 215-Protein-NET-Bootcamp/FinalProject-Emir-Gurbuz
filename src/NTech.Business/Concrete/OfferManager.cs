using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    [SecuredOperation("User")]
    public class OfferManager : AsyncBaseService<Offer, OfferWriteDto, OfferReadDto>, IOfferService
    {
        private readonly IProductService _productService;
        private readonly ILanguageMessage _languageMessage;
        private readonly IOfferDal _offerDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        private readonly IConfiguration _configuration;
        public OfferManager(IOfferDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IProductService productService, IOfferDal offerDal, IMessageBrokerHelper messageBrokerHelper, IConfiguration configuration) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _languageMessage = languageMessage;
            _productService = productService;
            _offerDal = offerDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _unitOfWork = unitOfWork;
            _messageBrokerHelper = messageBrokerHelper;
            _configuration = configuration;
        }

        [ValidationAspect(typeof(OfferWriteDtoValidator))]
        public async override Task<IResult> AddAsync(OfferWriteDto dto)
        {
            var result = BusinessRule.Run(
                await checkOfferedPriceGreaterThanProductPriceAsync(dto),
                await checkOfferAsync(dto),
                await checkProductIsSoldAsync(dto),
                await checkProductIsOfferableAsync(dto));

            if (dto.Percent != null)
                dto.OfferedPrice = (decimal)(dto.OfferedPrice * dto.Percent / 100);

            if (result != null)
                return result;

            return await base.AddAsync(dto);
        }

        [ValidationAspect(typeof(OfferWriteDtoValidator))]
        public override async Task<IResult> UpdateAsync(int id, OfferWriteDto dto)
        {
            var result = BusinessRule.Run(
                await checkOfferedPriceGreaterThanProductPriceAsync(dto),
                await checkOfferAsync(dto),
                await checkProductIsSoldAsync(dto),
                await checkProductIsOfferableAsync(dto));
            if (result != null)
                return result;

            return await base.UpdateAsync(id, dto);
        }

        private async Task<IResult> checkOfferedPriceGreaterThanProductPriceAsync(OfferWriteDto dto)
        {
            ProductReadDto product = (await _productService.GetByIdAsync(dto.ProductId)).Data;
            if (dto.OfferedPrice > product.Price)
                return new ErrorResult(_languageMessage.OfferedPriceCannotBeHigherThanProductPrice);

            return new SuccessResult();
        }
        private async Task<IResult> checkOfferAsync(OfferWriteDto dto)
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            
            var offer = await _offerDal.GetAsync(x => x.ProductId == dto.ProductId && x.UserId == userId);
            if (offer == null)
                return new SuccessResult();

            return new ErrorResult(_languageMessage.OfferIsAlreadyExists);
        }
        private async Task<IResult> checkProductIsSoldAsync(OfferWriteDto dto)
        {
            ProductReadDto product = (await _productService.GetByIdAsync(dto.ProductId)).Data;
            if (product.IsSold)
                return new ErrorResult(_languageMessage.ProductHasBeenSold);

            return new SuccessResult();
        }
        private async Task<IResult> checkProductIsOfferableAsync(OfferWriteDto dto)
        {
            ProductReadDto product = (await _productService.GetByIdAsync(dto.ProductId)).Data;
            if (product.isOfferable)
                return new ErrorResult(_languageMessage.CannotBeOffer);

            return new SuccessResult();
        }

        public async Task<IDataResult<List<OfferReadDto>>> GetSentOffers()
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();

            List<Offer> offers = await _offerDal.GetAll(o => o.UserId == userId).ToListAsync();
            List<OfferReadDto> offerReadDtos = Mapper.Map<List<OfferReadDto>>(offers);

            return new SuccessDataResult<List<OfferReadDto>>(offerReadDtos, _languageMessage.SuccessfullyListed);
        }
        public async Task<IDataResult<List<OfferReadDto>>> GetReceivedOffers()
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            
            List<Offer> offers = await _offerDal.GetAll(o => o.Product.UserId == userId).ToListAsync();
            List<OfferReadDto> offerReadDtos = Mapper.Map<List<OfferReadDto>>(offers);

            return new SuccessDataResult<List<OfferReadDto>>(offerReadDtos, _languageMessage.SuccessfullyListed);
        }

        public async Task<IResult> AcceptOffer(int offerId)
        {
            Offer offer = await _offerDal.GetAsync(o => o.Id == offerId);
            if (offer == null)
                return new ErrorResult(_languageMessage.NotFound);

            offer.Status = true;
            await _offerDal.UpdateAsync(offer);

            await deleteOtherOffers(offer);

            int row = await _unitOfWork.CompleteAsync();
            if (row > 0)
            {
                sendEmail(offer, _configuration.GetSection("EmailMessages:AcceptOfferSubject").Value, _configuration.GetSection("EmailMessages:AcceptOfferBody").Value);
                return new SuccessResult(_languageMessage.AcceptOfferSuccess);
            }
            return new ErrorResult(_languageMessage.AcceptOfferFailed);
        }

        private async Task deleteOtherOffers(Offer offer)
        {
            //get denied or waited offers
            var offers = await _offerDal.GetAll(o => o.ProductId == offer.ProductId && o.Id != offer.Id).ToListAsync();
            foreach (Offer o in offers)
                await _offerDal.DeleteAsync(o);
        }

        public async Task<IResult> DenyOffer(int offerId)
        {
            Offer offer = await _offerDal.GetAsync(o => o.Id == offerId);
            if (offer == null)
                return new ErrorResult(_languageMessage.NotFound);

            offer.Status = false;
            await _offerDal.UpdateAsync(offer);

            int row = await _unitOfWork.CompleteAsync();
            if (row > 0)
            {
                sendEmail(offer, _configuration.GetSection("EmailMessages:DenyOfferSubject").Value, _configuration.GetSection("EmailMessages:DenyOfferBody").Value);
                return new SuccessResult(_languageMessage.DenyOfferSuccess);
            }
            return new ErrorResult(_languageMessage.DenyOfferFailed);
        }

        public async Task<IDataResult<Offer>> GetOfferByUserIdAndProductIdAsync(int productId)
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            var offer = await _offerDal.GetAsync(x => x.ProductId == productId && x.UserId == userId);
            
            if (offer == null)
                return new ErrorDataResult<Offer>(_languageMessage.NotFound);

            return new SuccessDataResult<Offer>(offer);
        }

        private void sendEmail(Offer offer, string subject, string body)
        {
            _messageBrokerHelper.QueueMessage(
                new EmailQueue
                {
                    Subject = subject,
                    Body = string.Format(body, offer.User.FirstName, offer.User.LastName),
                    Email = offer.User.Email
                });
        }
    }
}
