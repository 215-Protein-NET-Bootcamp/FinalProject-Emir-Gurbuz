using AutoMapper;
using Core.Entity.Concrete;
using Core.Utilities.MessageBrokers.RabbitMq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete.EmailQueue;
using System.Diagnostics;

namespace NTech.WebAPI.Worker.EmailSend
{
    public class ConsumerEmailWorker : BackgroundService
    {
        private readonly IMessageBrokerHelper _brokerHelper;
        private readonly IEmailQueueService _emailQueueService;
        private readonly IMapper _mapper;

        public ConsumerEmailWorker(IMessageBrokerHelper brokerHelper, IEmailQueueService emailQueueService, IMapper mapper)
        {
            _brokerHelper = brokerHelper;
            _emailQueueService = emailQueueService;
            _mapper = mapper;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(3));
                List<EmailQueueReadDto> emailQueueReadDtos = (await _emailQueueService.GetListAsync()).Data;
                foreach (EmailQueueReadDto emailQueueReadDto in emailQueueReadDtos)
                {
                    await Task.Delay(1000);

                    _brokerHelper.QueueMessage(_mapper.Map<EmailQueue>(emailQueueReadDto));
                    Debug.WriteLine($"Add queue email:{emailQueueReadDto.Email}");
                    await _emailQueueService.HardDeleteAsync(emailQueueReadDto.Id);
                }
            }
        }
    }
}
