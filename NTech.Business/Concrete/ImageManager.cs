using AutoMapper;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ImageManager : AsyncBaseService<Image, ImageWriteDto, ImageReadDto>
    {
        public ImageManager(IImageDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
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
    }
}
