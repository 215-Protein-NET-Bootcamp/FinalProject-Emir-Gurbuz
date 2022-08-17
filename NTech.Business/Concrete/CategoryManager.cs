using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Concrete
{
    public class CategoryManager : AsyncBaseService<Category, CategoryWriteDto, CategoryReadDto>, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _categoryDal = repository;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(CategoryWriteDtoValidator))]
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> AddAsync(CategoryWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        [SecuredOperation("Admin")]
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

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("CategoryReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }

        public async Task<IDataResult<List<CategoryReadDto>>> GetListByFilterAsync(CategoryFilterResource categoryFilterResource)
        {
            IQueryable<Category> categories = _categoryDal.GetAll();
            if (categoryFilterResource.Name != null)
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(categoryFilterResource.Name.ToLower()));
            }
            List<CategoryReadDto> categoryReadDtos = Mapper.Map<List<CategoryReadDto>>(await categories.ToListAsync());
            return new SuccessDataResult<List<CategoryReadDto>>(categoryReadDtos, LanguageMessage.SuccessfullyListed);
        }
    }
}
