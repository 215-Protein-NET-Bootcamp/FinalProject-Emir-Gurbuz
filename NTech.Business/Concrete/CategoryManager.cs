using AutoMapper;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class CategoryManager : AsyncBaseService<Category, CategoryWriteDto, CategoryReadDto>, ICategoryService
    {
        public CategoryManager(ICategoryDal repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }
    }
}
