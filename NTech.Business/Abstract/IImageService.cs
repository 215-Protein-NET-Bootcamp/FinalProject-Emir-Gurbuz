using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IImageService : IAsyncBaseService<Image, ImageWriteDto, ImageReadDto>
    {
        Task<IDataResult<Image>> UploadAsync(IFormFile formFile);
    }
}
