using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
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

        [ValidationAspect(typeof(BrandWriteDtoValidator))]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> AddAsync(BrandWriteDto dto)
        {
            return base.AddAsync(dto);
        }
        [ValidationAspect(typeof(BrandWriteDtoValidator))]
        [CacheRemoveAspect("BrandReadDto")]
        public override Task<IResult> UpdateAsync(int id, BrandWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }
        [CacheAspect<DataResult<List<BrandReadDto>>>]
        public override async Task<DataResult<List<BrandReadDto>>> GetListAsync()
        {
            var result = await base.GetListAsync();
            return result;
        }
    }
}
