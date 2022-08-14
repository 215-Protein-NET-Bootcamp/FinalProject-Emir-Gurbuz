using AutoMapper;
using Core.Entity.Concrete;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete.EmailQueue;

namespace NTech.Business.Concrete
{
    public class EmailQueueManager : AsyncBaseService<EmailQueue, EmailQueueWriteDto, EmailQueueReadDto>, IEmailQueueService
    {
        public EmailQueueManager(IEmailQueueDal repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }
    }
}
