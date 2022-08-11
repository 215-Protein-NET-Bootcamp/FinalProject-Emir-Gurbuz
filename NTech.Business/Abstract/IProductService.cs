using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IProductService : IAsyncBaseService<Product, ProductWriteDto, ProductReadDto>
    {
    }
}
