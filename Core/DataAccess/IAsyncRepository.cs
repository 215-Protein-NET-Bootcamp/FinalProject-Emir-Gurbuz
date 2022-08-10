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
        Task<IQueryable<TEntity>> GetAllAsync(bool tracking = true);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    }
}
