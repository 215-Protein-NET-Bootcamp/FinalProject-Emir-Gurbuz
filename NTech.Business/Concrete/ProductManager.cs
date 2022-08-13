using AutoMapper;
using Core.Aspect.Autofac.Validation;
using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.URI;
using NTech.Business.Abstract;
using NTech.Business.Helpers;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ProductManager : AsyncBaseService<Product, ProductWriteDto, ProductReadDto>, IProductService
    {
        private readonly IUriService _uriService;
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IUriService uriService) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _uriService = uriService;
        }
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        public override Task<IResult> AddAsync(ProductWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        public async Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetPaginationAsync(PaginationFilter paginationFilter, string route)
        {
            var products = Mapper.Map<List<ProductReadDto>>(Repository.GetAll(false).ToList());
            return PaginationHelper.CreatePaginatedResponse(products, paginationFilter, products.Count(), _uriService, route);
        }

        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        public override Task<IResult> UpdateAsync(int id, ProductWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }
    }
}
