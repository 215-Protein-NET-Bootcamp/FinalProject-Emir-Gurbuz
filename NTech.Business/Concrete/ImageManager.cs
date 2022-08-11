using AutoMapper;
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
    }
}
