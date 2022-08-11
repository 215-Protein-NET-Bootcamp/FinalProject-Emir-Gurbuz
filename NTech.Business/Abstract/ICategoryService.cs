using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface ICategoryService : IAsyncBaseService<Category, CategoryWriteDto, CategoryReadDto>
    {
    }
}
