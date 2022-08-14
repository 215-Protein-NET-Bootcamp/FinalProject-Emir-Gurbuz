using Core.Entity.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Newtonsoft.Json;
using NTech.Business.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace NTech.WebAPI.Worker.EmailSend
{
    public class SendEmailWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IEmailSender _mailService;
        private readonly IMessageBrokerHelper _brokerHelper;
        private readonly IEmailQueueService _emailQueueService;
        public SendEmailWorker(IConfiguration configuration, IMessageConsumer messageConsumer, IEmailSender mailSender, IMessageBrokerHelper brokerHelper)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            _messageConsumer = messageConsumer;
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
                    queue: "NTechQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1200);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (model, mq) =>
                    {
                        var body = mq.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        EmailQueue emailQueue = JsonConvert.DeserializeObject<EmailQueue>(message);
                        try
                        {
                            Debug.WriteLine(emailQueue.TryCount);
                            if (emailQueue.TryCount >= 5)
                            {
                                emailQueue.TryCount = 0;
                                await _emailQueueService.AddAsync(emailQueue);
                                return;
                            }
                            await _mailService.SendEmailAsync(new EmailMessage
                            {
                                Body = emailQueue.Body,
                                Email = emailQueue.Email,
                                Subject = emailQueue.Subject
                            });
                        }
                        catch (Exception e)
                        {
                            emailQueue.TryCount++;
                            _brokerHelper.QueueMessage(emailQueue);
                        }
                    };
                    channel.BasicConsume(
                            queue: "NTechQueue",
                    autoAck: true,
                    consumer: consumer);
                }
            }
        }
    }
}