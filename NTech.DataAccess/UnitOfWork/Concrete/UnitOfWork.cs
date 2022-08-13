using Microsoft.EntityFrameworkCore;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool disposed;
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //_dbContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
