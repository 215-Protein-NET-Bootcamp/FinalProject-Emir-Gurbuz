using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IColorService : IAsyncBaseService<Color, ColorWriteDto, ColorReadDto>
    {
    }
}
