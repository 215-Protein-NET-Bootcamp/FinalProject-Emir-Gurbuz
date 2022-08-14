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
    public class CategoryManager : AsyncBaseService<Category, CategoryWriteDto, CategoryReadDto>, ICategoryService
    {
        public CategoryManager(ICategoryDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }

        [ValidationAspect(typeof(CategoryWriteDtoValidator))]
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> AddAsync(CategoryWriteDto dto)
        {
            return base.AddAsync(dto);
        }
        [ValidationAspect(typeof(CategoryWriteDtoValidator))]
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> UpdateAsync(int id, CategoryWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }
        [CacheAspect<DataResult<List<CategoryReadDto>>>()]
        public override Task<DataResult<List<CategoryReadDto>>> GetListAsync()
        {
            return base.GetListAsync();
        }
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }
    }
}
