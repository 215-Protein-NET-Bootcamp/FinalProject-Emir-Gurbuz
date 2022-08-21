using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Business;
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
        public override async Task<IResult> AddAsync(UsingStatusWriteDto dto)
        {
            IResult result = BusinessRule.Run(
                await usingStatusExistsAsync(dto));
            if (result != null)
                return result;

            return await base.AddAsync(dto);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UsingStatusWriteDtoValidator))]
        public override async Task<IResult> UpdateAsync(int id, UsingStatusWriteDto dto)
        {
            IResult result = BusinessRule.Run(
                await usingStatusExistsAsync(dto));
            if (result != null)
                return result;

            return await base.UpdateAsync(id, dto);
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

        private async Task<IResult> usingStatusExistsAsync(UsingStatusWriteDto dto)
        {
            UsingStatus usingStatus = await _usingStatusDal.GetAsync(u => u.Status.ToLower() == dto.Status.ToLower());
            if (usingStatus != null)
                return new ErrorResult(LanguageMessage.UsingStatusIsAlreadyExists);
            return new SuccessResult();
        }
    }
}
