using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfAsyncBaseRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
