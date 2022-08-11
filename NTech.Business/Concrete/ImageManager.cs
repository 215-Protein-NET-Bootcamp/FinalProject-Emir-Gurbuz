using AutoMapper;
using Core.DataAccess;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ImageManager : AsyncBaseService<Image, ImageWriteDto, ImageReadDto>
    {
        public ImageManager(IImageDal repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }
    }
}
