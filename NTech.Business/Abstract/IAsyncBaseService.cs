using Core.Dto;
using Core.Entity;
using Core.Utilities.Result;

namespace NTech.Business.Abstract
{
    public interface IAsyncBaseService<TEntity, TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        Task<IResult> AddAsync(TDto dto);
        Task<IResult> UpdateAsync(int id, TDto dto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<TDto>> GetByIdAsync(int id);
        Task<IDataResult<List<TDto>>> GetListAsync();
    }
}
