using Core.Entity;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
        IQueryable<TEntity> GetAll(bool tracking = true);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    }
}
