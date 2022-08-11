using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IImageService : IAsyncBaseService<Image, ImageWriteDto, ImageReadDto>
    {
    }
}
