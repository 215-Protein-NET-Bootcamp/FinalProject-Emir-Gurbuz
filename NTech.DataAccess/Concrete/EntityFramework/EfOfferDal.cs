using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfOfferDal : EfAsyncBaseRepository<Offer>, IOfferDal
    {
        public EfOfferDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
