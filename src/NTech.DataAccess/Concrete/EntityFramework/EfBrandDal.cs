using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfAsyncBaseRepository<Brand>, IBrandDal
    {
        public EfBrandDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
