using AutoMapper;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    [SecuredOperation("User")]
    public class ImageManager : AsyncBaseService<Image, ImageWriteDto, ImageReadDto>, IImageService
    {
        private readonly IConfiguration configuration;
        private readonly ILanguageMessage _languageMessage;
        public ImageManager(IImageDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IConfiguration configuration) : base(repository, mapper, unitOfWork, languageMessage)
        {
            this.configuration = configuration;
            _languageMessage = languageMessage;
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
                return new ErrorDataResult<Image>(_languageMessage.FileIsNotNull);

            string databasePath = configuration.GetSection("UploadImagePath").Value;

            if (formFile.Length / 1024f > 400) // check file size
                return new ErrorDataResult<Image>(_languageMessage.FileSizeIsHigh);

            string ex = Path.GetExtension(formFile.FileName);
            if ((ex == ".jpg" || ex == ".jpeg" || ex == ".png") == false)
                return new ErrorDataResult<Image>(_languageMessage.UnSupportedFile);

            string fileName = $"{Guid.NewGuid()}{ex}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), databasePath);

            if (File.Exists(path) == false)
                Directory.CreateDirectory(path);

            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write, FileShare.None, 1024, useAsync: false))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();

                Image image = new()
                {
                    Path = $"{databasePath}{fileName}"
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
