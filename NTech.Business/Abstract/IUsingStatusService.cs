using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IUsingStatusService : IAsyncBaseService<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>
    {
    }
}
