using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfEmailQueueDal : EfAsyncBaseRepository<EmailQueue>, IEmailQueueDal
    {
        public EfEmailQueueDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
