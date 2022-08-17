using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Abstract
{
    public interface IColorService : IAsyncBaseService<Color, ColorWriteDto, ColorReadDto>
    {
        Task<IDataResult<List<ColorReadDto>>> GetListByFilterAsync(ColorFilterResource colorFilterResouce);
    }
}
