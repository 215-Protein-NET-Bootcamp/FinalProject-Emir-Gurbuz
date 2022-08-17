using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;
using System.Linq.Expressions;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfAsyncBaseRepository<Product>, IProductDal
    {
        public EfProductDal(DbContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<Product> GetAll(bool tracking = true)
        {
            return base.GetAll(tracking)
                .Include(p => p.Color)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.UsingStatus)
                .Include(p => p.Image);
        }
        public override IQueryable<Product> GetAll(Expression<Func<Product, bool>> predicate, bool tracking = true)
        {
            return base.GetAll(predicate, tracking)
                .Include(p => p.Color)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.UsingStatus)
                .Include(p => p.Image);
        }
        public async override Task<Product> GetAsync(Expression<Func<Product, bool>> predicate, bool tracking = true)
        {
            var table = _context.Set<Product>()
                .Include(p => p.Color)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.UsingStatus)
                .Include(p => p.Image);
            return tracking ?
                await table.SingleOrDefaultAsync(predicate) :
                await table.AsNoTracking().SingleOrDefaultAsync(predicate);
        }
    }
}
