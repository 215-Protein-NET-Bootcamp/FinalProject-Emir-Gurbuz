using AutoMapper;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ImageManager : AsyncBaseService<Image, ImageWriteDto, ImageReadDto>, IImageService
    {
        private readonly IConfiguration configuration;
        public ImageManager(IImageDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IConfiguration configuration) : base(repository, mapper, unitOfWork, languageMessage)
        {
            this.configuration = configuration;
        }

        public override Task<IResult> AddAsync(ImageWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        public override Task<IResult> UpdateAsync(int id, ImageWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }

        public override Task<DataResult<List<ImageReadDto>>> GetListAsync()
        {
            return base.GetListAsync();
        }

        public override Task<DataResult<ImageReadDto>> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public async Task<IDataResult<Image>> UploadAsync(IFormFile formFile)
        {
            if (formFile == null)
                return new ErrorDataResult<Image>();

            string databasePath = configuration.GetSection("UploadImagePath").Value;

            if (formFile.Length / 1024f > 400)
                return new ErrorDataResult<Image>();

            string ex = Path.GetExtension(formFile.Name);
            string fileName = $"{Guid.NewGuid()}.{ex}";
            string path = String.Format(Directory.GetCurrentDirectory(), databasePath, fileName);
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024, useAsync: false))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();

                Image image = new()
                {
                    Path = databasePath
                };
                await Repository.AddAsync(image);
                int row = await UnitOfWork.CompleteAsync();
                return row > 0 ?
                    new SuccessDataResult<Image>(image) :
                    new ErrorDataResult<Image>();
            }
        }
    }
}
