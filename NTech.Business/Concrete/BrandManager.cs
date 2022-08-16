﻿using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class BrandManager : AsyncBaseService<Brand, BrandWriteDto, BrandReadDto>, IBrandService
    {
        public BrandManager(IBrandDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(BrandWriteDtoValidator))]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> AddAsync(BrandWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(BrandWriteDtoValidator))]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> UpdateAsync(int id, BrandWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }

        [CacheAspect<DataResult<List<BrandReadDto>>>()]
        public override Task<DataResult<List<BrandReadDto>>> GetListAsync()
        {
            return base.GetListAsync();
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }
    }
}
