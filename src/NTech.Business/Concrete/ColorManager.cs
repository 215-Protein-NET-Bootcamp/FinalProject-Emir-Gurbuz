using AutoMapper;
using Core.Aspect.Autofac.Caching;
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
    public class ColorManager : AsyncBaseService<Color, ColorWriteDto, ColorReadDto>, IColorService
    {
        public ColorManager(IColorDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ColorWriteDtoValidator))]
        [CacheRemoveAspect("ColorReadDto")]
        public override async Task<IResult> AddAsync(ColorWriteDto dto)
        {
            IResult result = BusinessRule.Run(
                await colorNameExists(dto));
            if (result != null)
                return result;

            return await base.AddAsync(dto);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ColorWriteDtoValidator))]
        [CacheRemoveAspect("ColorReadDto")]
        public override async Task<IResult> UpdateAsync(int id, ColorWriteDto dto)
        {
            IResult result = BusinessRule.Run(
                await colorNameExists(dto));
            if (result != null)
                return result;

            return await base.UpdateAsync(id, dto);
        }

        [CacheAspect<DataResult<List<ColorReadDto>>>()]
        public override Task<DataResult<List<ColorReadDto>>> GetListAsync()
        {
            return base.GetListAsync();
        }

        public async Task<IDataResult<List<ColorReadDto>>> GetListByFilterAsync(ColorFilterResource colorFilterResouce)
        {
            IQueryable<Color> colors = Repository.GetAll();
            if (colorFilterResouce.Name != null)
            {
                colors = colors.Where(c => c.Name.ToLower().Contains(colorFilterResouce.Name.ToLower()));
            }
            List<ColorReadDto> colorReadDtos = Mapper.Map<List<ColorReadDto>>(await colors.ToListAsync());
            return new SuccessDataResult<List<ColorReadDto>>(colorReadDtos, LanguageMessage.SuccessfullyListed);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ColorReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ColorReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }

        private async Task<IResult> colorNameExists(ColorWriteDto dto)
        {
            Color color = await Repository.GetAsync(c => c.Name.ToLower() == dto.Name.ToLower());
            if (color != null)
                return new ErrorResult(LanguageMessage.ColorIsAlreadyExists);
            return new SuccessResult();
        }

    }
}
