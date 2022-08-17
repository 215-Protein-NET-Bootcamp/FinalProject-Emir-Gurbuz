using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.Abstract;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfImageDal : EfAsyncBaseRepository<Image>, IImageDal
    {
        public EfImageDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
