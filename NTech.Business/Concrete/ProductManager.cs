using AutoMapper;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class ProductManager : AsyncBaseService<Product, ProductWriteDto, ProductReadDto>, IProductService
    {
        public ProductManager(IProductDal repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }

        public override async Task<IDataResult<List<ProductReadDto>>> GetListAsync()
        {
            List<Product> entities = await Repository.GetAll()
                .Include(p => p.Color)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.UsingStatus)
                .Include(p=>p.Image)
                .ToListAsync();
            List<ProductReadDto> returnEntities = Mapper.Map<List<ProductReadDto>>(entities);

            return new SuccessDataResult<List<ProductReadDto>>(returnEntities);
        }
    }
}
