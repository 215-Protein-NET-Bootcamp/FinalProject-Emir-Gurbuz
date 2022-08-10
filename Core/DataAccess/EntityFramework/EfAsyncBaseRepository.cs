using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfAsyncBaseRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public DbContext _context { get; set; }
        public EfAsyncBaseRepository(DbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<bool> AddAsync(TEntity entity)
        {
            EntityEntry entityEntry = await _context.Set<TEntity>().AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await Task.Run(() =>
            {
                EntityEntry entityEntry = _context.Set<TEntity>().Remove(entity);
                return entityEntry.State == EntityState.Deleted;
            });
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(bool tracking = true)
        {
            return await Task.Run(() =>
            {
                return tracking ?
                _context.Set<TEntity>().AsQueryable() :
                _context.Set<TEntity>().AsNoTracking().AsQueryable();
            });
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            return await Task.Run(() =>
            {
                return tracking ?
                _context.Set<TEntity>().Where(predicate).AsQueryable() :
                _context.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable();
            });
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            return tracking ?
                await _context.Set<TEntity>().SingleOrDefaultAsync(predicate) :
                await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() =>
            {
                EntityEntry entityEntry = _context.Set<TEntity>().Update(entity);
                return entityEntry.State == EntityState.Modified;
            });
        }
    }
}
