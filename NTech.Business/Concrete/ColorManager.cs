using AutoMapper;
using Core.DataAccess;
using NTech.Business.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ColorManager : AsyncBaseService<Color, ColorWriteDto, ColorReadDto>, IColorService
    {
        public ColorManager(IAsyncRepository<Color> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }
    }
}
