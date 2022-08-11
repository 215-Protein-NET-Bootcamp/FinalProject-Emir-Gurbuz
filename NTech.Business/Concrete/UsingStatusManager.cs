using AutoMapper;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class UsingStatusManager : AsyncBaseService<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>, IUsingStatusService
    {
        public UsingStatusManager(IUsingStatusDal repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }
    }
}
