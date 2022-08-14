using AutoMapper;
using Core.DataAccess;
using Core.Utilities.Business;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
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
        public OfferManager(IOfferDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IProductService productService) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _languageMessage = languageMessage;
            _productService = productService;
        }
        public override Task<IResult> AddAsync(OfferWriteDto dto)
        {
            var result = BusinessRule.Run(
                checkOfferedPriceGreaterThanProductPrice(dto));
            if (result != null)
                return Task.Run(() => result);

            return base.AddAsync(dto);
        }

        public override Task<IResult> UpdateAsync(int id, OfferWriteDto dto)
        {
            var result = BusinessRule.Run(
                checkOfferedPriceGreaterThanProductPrice(dto));
            if (result != null)
                return Task.Run(() => result);

            return base.UpdateAsync(id, dto);
        }

        private IResult checkOfferedPriceGreaterThanProductPrice(OfferWriteDto dto)
        {
            ProductReadDto product = _productService.GetByIdAsync(dto.ProductId).Result.Data;
            if (dto.OfferedPrice > product.Price)
                return new ErrorResult(_languageMessage.OfferedPriceCannotBeHigherThanProductPrice);
            return new SuccessResult();
        }
    }
}
