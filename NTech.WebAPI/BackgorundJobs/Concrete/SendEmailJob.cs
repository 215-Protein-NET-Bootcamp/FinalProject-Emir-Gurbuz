using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using System.Diagnostics;

namespace NTech.WebAPI.BackgorundJobs
{
    public class SendEmailJob : IBackgroundJob
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IEmailSender _mailService;
        public SendEmailJob()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            _messageConsumer = ServiceTool.ServiceProvider.GetService<IMessageConsumer>();
            _mailService = ServiceTool.ServiceProvider.GetService<IEmailSender>();
        }
        public void Run()
        {
            _messageConsumer.GetQueue(async (message) =>
            {
                await _mailService.SendEmailAsync(new EmailMessage
                {

                });
            });
        }
    }
}
