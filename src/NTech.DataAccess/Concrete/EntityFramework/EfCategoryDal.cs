using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;
using System.Linq.Expressions;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfAsyncBaseRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(DbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Category> GetAsync(Expression<Func<Category, bool>> predicate, bool tracking = true)
        {
            var table = _context.Set<Category>()
                .Include(c => c.Products);
            return tracking ?
                await table.SingleOrDefaultAsync(predicate) :
                await table.AsNoTracking().SingleOrDefaultAsync(predicate);
        }
    }
}
