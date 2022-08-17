using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Abstract
{
    public interface IBrandService : IAsyncBaseService<Brand, BrandWriteDto, BrandReadDto>
    {
        Task<IDataResult<List<BrandReadDto>>> GetListByFilterAsync(BrandFilterResource brandFilterResource);
    }
}
