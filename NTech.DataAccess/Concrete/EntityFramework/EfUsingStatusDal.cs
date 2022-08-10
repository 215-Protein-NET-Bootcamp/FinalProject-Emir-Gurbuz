using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfUsingStatusDal : EfAsyncBaseRepository<UsingStatus>, IUsingStatusDal
    {
        public EfUsingStatusDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
