using Core.Entity.Concrete;
using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IProductService : IAsyncBaseService<Product, ProductWriteDto, ProductReadDto>
    {
        Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetPaginationAsync(PaginationFilter paginationFilter, string route);
        Task<IResult> BuyAsync(int productId);
        Task<IResult> SetImageAsync(int productId, int imageId);
    }
}
