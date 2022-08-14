using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.URI;
using Newtonsoft.Json;
using NTech.Business.Abstract;
using NTech.Business.BusinessAspects;
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
        private readonly ICacheManager _cacheManager;
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage, IUriService uriService, ICacheManager cacheManager) : base(repository, mapper, unitOfWork, languageMessage)
        {
            _uriService = uriService;
            _cacheManager = cacheManager;
        }
        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> AddAsync(ProductWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        [ValidationAspect(typeof(ProductWriteDtoValidator))]
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> UpdateAsync(int id, ProductWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }

        [SecuredOperation("User")]
        public async Task<PaginatedResult<IEnumerable<ProductReadDto>>> GetPaginationAsync(PaginationFilter paginationFilter, string route)
        {
            string key = $"product+{paginationFilter.PageNumber}+{paginationFilter.PageSize}";
            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<PaginatedResult<IEnumerable<ProductReadDto>>>(key);
            }
            var products = Mapper.Map<List<ProductReadDto>>(Repository.GetAll(false).ToList());
            var result = PaginationHelper.CreatePaginatedResponse(products, paginationFilter, products.Count(), _uriService, route);
            _cacheManager.Add(key, result, 30);
            return result;
        }

        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> SoftDeleteAsync(int id)
        {
            return base.SoftDeleteAsync(id);
        }
        [CacheRemoveAspect("ProductReadDto")]
        public override Task<IResult> HardDeleteAsync(int id)
        {
            return base.HardDeleteAsync(id);
        }
    }
}
