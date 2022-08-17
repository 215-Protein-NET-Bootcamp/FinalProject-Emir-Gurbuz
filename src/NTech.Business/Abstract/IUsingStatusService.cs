using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Abstract
{
    public interface IUsingStatusService : IAsyncBaseService<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>
    {
        Task<IDataResult<List<UsingStatusReadDto>>> GetListByFilterAsync(UsingStatusFilterResource usingStatusFilterResource);
    }
}
