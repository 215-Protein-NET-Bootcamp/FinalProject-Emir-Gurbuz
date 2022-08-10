using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfAsyncBaseRepository<Product>, IProductDal
    {
        public EfProductDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
