using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Concrete
{
    public class UsingStatusManager : AsyncBaseService<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>, IUsingStatusService
    {
        private readonly IUsingStatusDal _usingStatusDal;
        public UsingStatusManager(IUsingStatusDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _usingStatusDal = repository;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UsingStatusWriteDtoValidator))]
        public override Task<IResult> AddAsync(UsingStatusWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UsingStatusWriteDtoValidator))]
        public override Task<IResult> UpdateAsync(int id, UsingStatusWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }

        [SecuredOperation("Admin")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }

        [SecuredOperation("Admin")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }

        public async Task<IDataResult<List<UsingStatusReadDto>>> GetListByFilterAsync(UsingStatusFilterResource usingStatusFilterResource)
        {
            IQueryable<UsingStatus> usingStatuses = _usingStatusDal.GetAll();
            if (usingStatusFilterResource.Status != null)
            {
                usingStatuses = usingStatuses.Where(u => u.Status.ToLower().Contains(usingStatusFilterResource.Status.ToLower()));
            }
            List<UsingStatusReadDto> usingStatusReadDtos = Mapper.Map<List<UsingStatusReadDto>>(await usingStatuses.ToListAsync());
            return new SuccessDataResult<List<UsingStatusReadDto>>(usingStatusReadDtos, LanguageMessage.SuccessfullyListed);
        }
    }
}
