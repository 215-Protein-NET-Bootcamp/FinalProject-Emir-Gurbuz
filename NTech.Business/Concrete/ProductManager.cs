using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ProductManager : AsyncBaseService<Product, ProductWriteDto, ProductReadDto>, IProductService
    {
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        public override Task<IResult> AddAsync(ProductWriteDto dto)
        {
            return base.AddAsync(dto);
        }
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        public override Task<IResult> UpdateAsync(int id, ProductWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }
    }
}
