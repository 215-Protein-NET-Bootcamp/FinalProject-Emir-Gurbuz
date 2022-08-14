using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;
using System.Linq.Expressions;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfOfferDal : EfAsyncBaseRepository<Offer>, IOfferDal
    {
        public EfOfferDal(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Offer> GetAsync(Expression<Func<Offer, bool>> predicate, bool tracking = true)
        {
            var table = _context.Set<Offer>()
                .Include(o => o.Product)
                .Include(o => o.User);
            return tracking ?
                await table.SingleOrDefaultAsync(predicate) :
                await table.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public override IQueryable<Offer> GetAll(bool tracking = true)
        {
            return base.GetAll(tracking)
                .Include(o => o.Product)
                .Include(o => o.User);
        }

        public override IQueryable<Offer> GetAll(Expression<Func<Offer, bool>> predicate, bool tracking = true)
        {
            return base.GetAll(predicate, tracking)
                .Include(o => o.Product)
                .Include(o => o.User);
        }
    }
}
