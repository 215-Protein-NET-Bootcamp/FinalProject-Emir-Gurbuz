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

        public virtual IQueryable<TEntity> GetAll(bool tracking = true)
        {
            var table = _context.Set<TEntity>()
                .Where(x => x.DeletedDate.HasValue == false)
                .OrderBy(x => x.Id);
            return tracking ?
            table.AsQueryable() :
            table.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var table = _context.Set<TEntity>()
                .Where(x => x.DeletedDate.HasValue == false)
                .OrderBy(x => x.Id);
            return tracking ?
            table.Where(predicate).AsQueryable() :
            table.Where(predicate).AsNoTracking().AsQueryable();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var table = _context.Set<TEntity>().Where(x => x.DeletedDate.HasValue == false);
            return tracking ?
                await table.SingleOrDefaultAsync(predicate) :
                await table.AsNoTracking().SingleOrDefaultAsync(predicate);
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
