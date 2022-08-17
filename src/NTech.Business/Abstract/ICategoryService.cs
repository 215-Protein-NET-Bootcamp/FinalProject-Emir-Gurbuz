using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Abstract
{
    public interface ICategoryService : IAsyncBaseService<Category, CategoryWriteDto, CategoryReadDto>
    {
        Task<IDataResult<List<CategoryReadDto>>> GetListByFilterAsync(CategoryFilterResource categoryFilterResource);
    }
}
