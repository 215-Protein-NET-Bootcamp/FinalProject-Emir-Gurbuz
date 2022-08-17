using Core.Entity.Concrete;
using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Abstract
{
    public interface IProductService : IAsyncBaseService<Product, ProductWriteDto, ProductReadDto>
    {
        Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetPaginationAsync(PaginationFilter paginationFilter, string route);
        Task<IResult> BuyAsync(int productId);
        Task<IResult> SetImageAsync(int productId, IFormFile file);

        Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetListByFilterAsync(PaginationFilter paginationFilter, string route, ProductFilterResource productFilterResource);
    }
}
