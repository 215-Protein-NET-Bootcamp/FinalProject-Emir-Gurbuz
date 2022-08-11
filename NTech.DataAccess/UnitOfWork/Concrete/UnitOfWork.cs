using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NTech.DataAccess.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTech.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction _dbContextTransaction;
        private bool disposed;
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        protected virtual void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }
            disposed = true;
        }
    }
}
