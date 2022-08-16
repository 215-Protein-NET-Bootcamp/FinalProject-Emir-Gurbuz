using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class UsingStatusManager : AsyncBaseService<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>, IUsingStatusService
    {
        public UsingStatusManager(IUsingStatusDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
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
    }
}
