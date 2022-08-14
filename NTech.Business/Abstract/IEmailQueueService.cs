using Core.Entity.Concrete;
using NTech.Dto.Concrete.EmailQueue;

namespace NTech.Business.Abstract
{
    public interface IEmailQueueService : IAsyncBaseService<EmailQueue, EmailQueueWriteDto, EmailQueueReadDto>
    {
    }
}
