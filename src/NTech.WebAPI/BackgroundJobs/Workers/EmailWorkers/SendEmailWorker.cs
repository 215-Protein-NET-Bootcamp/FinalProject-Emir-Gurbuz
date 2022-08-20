using Core.Entity.Concrete;
using Core.Enums;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Newtonsoft.Json;
using NTech.Business.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace NTech.WebAPI.BackgorundJobs.Workers
{
    public class SendEmailWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IEmailSender _mailService;
        private readonly IMessageBrokerHelper _brokerHelper;
        private readonly IEmailQueueService _emailQueueService;
        public SendEmailWorker(IConfiguration configuration, IEmailSender mailSender, IMessageBrokerHelper brokerHelper)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            _mailService = mailSender;
            _brokerHelper = brokerHelper;
            _emailQueueService = ServiceTool.ServiceProvider.GetService<IEmailQueueService>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: QueueNameEnum.EmailQueue.ToString(),
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1200);
                    var consumer = new EventingBasicConsumer(channel);
                    await consumerEmailAsync(consumer, channel);
                }
            }
        }
        private async Task consumerEmailAsync(EventingBasicConsumer consumer, IModel channel)
        {
            consumer.Received += async (model, mq) =>
            {
                string message = getMessageString(mq);
                EmailQueue emailQueue = JsonConvert.DeserializeObject<EmailQueue>(message);
                try
                {
                    if (emailQueue.TryCount >= 5)
                    {
                        emailQueue.TryCount = 0;
                        await _emailQueueService.AddAsync(emailQueue);
                        Debug.WriteLine($"Add database failed email:{emailQueue.Email}");
                        return;
                    }
                    await _mailService.SendEmailAsync(new EmailMessage
                    {
                        Body = emailQueue.Body,
                        Email = emailQueue.Email,
                        Subject = emailQueue.Subject
                    });
                    Debug.WriteLine($"Successful sended email:{emailQueue.Email}");
                }
                catch (Exception e)
                {
                    emailQueue.TryCount++;
                    Debug.WriteLine($"{emailQueue.TryCount} failed send email:{emailQueue.Email}");
                    _brokerHelper.QueueMessage(QueueNameEnum.EmailQueue.ToString(), emailQueue);
                }
            };
            channel.BasicConsume(
                    queue: QueueNameEnum.EmailQueue.ToString(),
            autoAck: true,
            consumer: consumer);
        }
        private string getMessageString(BasicDeliverEventArgs mq)
        {
            byte[] body = mq.Body.ToArray();
            return Encoding.UTF8.GetString(body);
        }
    }
}