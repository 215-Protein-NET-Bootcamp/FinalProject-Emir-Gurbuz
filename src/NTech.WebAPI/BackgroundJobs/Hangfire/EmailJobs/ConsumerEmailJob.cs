using AutoMapper;
using Core.Entity.Concrete;
using Core.Enums;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete.EmailQueue;
using System.Diagnostics;

namespace NTech.WebAPI.BackgorundJobs.Hangfire
{
    public class ConsumerEmailJob : IBackgroundJob
    {
        private readonly IMessageBrokerHelper _brokerHelper;
        private readonly IEmailQueueService _emailQueueService;
        private readonly IMapper _mapper;

        public ConsumerEmailJob()
        {
            _mapper = ServiceTool.ServiceProvider.GetService<IMapper>();
            _emailQueueService = ServiceTool.ServiceProvider.GetService<IEmailQueueService>();
            _brokerHelper = ServiceTool.ServiceProvider.GetService<IMessageBrokerHelper>();
        }

        public async Task Run(TimeSpan duration)
        {
            while (true)
            {
                await Task.Delay(duration);
                Debug.WriteLine("consumer email job");
                List<EmailQueueReadDto> emailQueueReadDtos = (await _emailQueueService.GetListAsync()).Data;
                foreach (EmailQueueReadDto emailQueueReadDto in emailQueueReadDtos)
                {
                    await Task.Delay(1000);

                    _brokerHelper.QueueMessage(QueueNameEnum.EmailQueue.ToString(), _mapper.Map<EmailQueue>(emailQueueReadDto));
                    Debug.WriteLine($"Add queue email:{emailQueueReadDto.Email}");
                    await _emailQueueService.HardDeleteAsync(emailQueueReadDto.Id);
                }
            }
        }
    }
}
