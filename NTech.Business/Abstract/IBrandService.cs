using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IBrandService : IAsyncBaseService<Brand, BrandWriteDto, BrandReadDto>
    {
    }
}
