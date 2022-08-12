using Core.Entity.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Newtonsoft.Json;

namespace NTech.WebAPI.BackgorundJobs
{
    public class SendEmailJob : IBackgroundJob
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IEmailSender _mailService;
        private readonly IMessageBrokerHelper _brokerHelper;
        public SendEmailJob()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            _messageConsumer = ServiceTool.ServiceProvider.GetService<IMessageConsumer>();
            _mailService = ServiceTool.ServiceProvider.GetService<IEmailSender>();
            _brokerHelper = ServiceTool.ServiceProvider.GetService<IMessageBrokerHelper>();
        }
        public void Run()
        {
            _messageConsumer.GetQueue(async (message) =>
            {
                EmailQueue emailQueue = JsonConvert.DeserializeObject<EmailQueue>(message);
                try
                {
                    if (emailQueue.TryCount >= 5)
                    {
                        //TODO: change status
                        return;
                    }
                    await _mailService.SendEmailAsync(new EmailMessage
                    {
                        Body = emailQueue.Body,
                        Email = emailQueue.Email,
                        Subject = emailQueue.Subject
                    });
                }
                catch (Exception)
                {
                    emailQueue.TryCount++;
                    _brokerHelper.QueueMessage(emailQueue);
                }
            });
        }
    }
}
