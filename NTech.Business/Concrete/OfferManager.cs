﻿using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.DataAccess;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.AspNetCore.Http;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace NTech.Business.Concrete
{
    [SecuredOperation("User")]
    public class OfferManager : AsyncBaseService<Offer, OfferWriteDto, OfferReadDto>, IOfferService
    {
        private readonly IProductService _productService;
        private readonly ILanguageMessage _languageMessage;
        private readonly IOfferDal _offerDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OfferManager(IOfferDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IProductService productService, IOfferDal offerDal) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _languageMessage = languageMessage;
            _productService = productService;
            _offerDal = offerDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [ValidationAspect(typeof(OfferWriteDtoValidator))]
        public override Task<IResult> AddAsync(OfferWriteDto dto)
        {
            var result = BusinessRule.Run(
                checkOfferedPriceGreaterThanProductPrice(dto),
                checkOffer(dto));
            if (result != null)
                return Task.Run(() => result);

            return base.AddAsync(dto);
        }


        [ValidationAspect(typeof(OfferWriteDtoValidator))]
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
        private IResult checkOffer(OfferWriteDto dto)
        {
            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            Offer offerReadDto = _offerDal.GetAsync(x => x.ProductId == dto.ProductId && x.UserId == userId).Result;
            if (offerReadDto == null)
                return new SuccessResult();
            return new ErrorResult(_languageMessage.OfferIsAlreadyExists);
        }
    }
}
