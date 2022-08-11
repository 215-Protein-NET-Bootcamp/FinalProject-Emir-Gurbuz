using Core.Dto;
using Core.Entity;
using Core.Utilities.Result;

namespace NTech.Business.Abstract
{
    public interface IAsyncBaseService<TEntity, TWriteDto, TReadDto>
        where TEntity : class, IEntity, new()
        where TWriteDto : class, IWriteDto, new()
        where TReadDto : class, IReadDto, new()
    {
        Task<IResult> AddAsync(TWriteDto dto);
        Task<IResult> UpdateAsync(int id, TWriteDto dto);
        Task<IResult> SoftDeleteAsync(int id);
        Task<IResult> HardDeleteAsync(int id);
        Task<IDataResult<TReadDto>> GetByIdAsync(int id);
        Task<IDataResult<List<TReadDto>>> GetListAsync();
    }
}
