using Core.Entity.Concrete;
using Core.Utilities.Result;
using NTech.Dto.Concrete.EmailQueue;

namespace NTech.Business.Abstract
{
    public interface IEmailQueueService : IAsyncBaseService<EmailQueue, EmailQueueWriteDto, EmailQueueReadDto>
    {
        Task<IResult> AddAsync(EmailQueue emailQueue);
    }
}
