using Core.Entity.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, mq) =>
                {
                    var body = mq.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    EmailQueue emailQueue = JsonConvert.DeserializeObject<EmailQueue>(message);
                    var task = _mailService.SendEmailAsync(new EmailMessage
                    {
                        Body = emailQueue.Body,
                        Email = emailQueue.Email,
                        Subject = emailQueue.Subject
                    });
                    task.Wait();
                };
                channel.BasicConsume(
                        queue: "NTechQueue",
                autoAck: true,
                consumer: consumer);
            }
            //_messageConsumer.GetQueue((message) =>
            //{
            //    EmailQueue emailQueue = JsonConvert.DeserializeObject<EmailQueue>(message);
            //    try
            //    {
            //        if (emailQueue.TryCount >= 5)
            //        {
            //            //TODO: change status
            //            return;
            //        }
            //        Debug.WriteLine("gönderildi");
            //        var task = _mailService.SendEmailAsync(new EmailMessage
            //        {
            //            Body = emailQueue.Body,
            //            Email = emailQueue.Email,
            //            Subject = emailQueue.Subject
            //        });
            //        task.Wait();
            //    }
            //    catch (Exception)
            //    {
            //        emailQueue.TryCount++;
            //        _brokerHelper.QueueMessage(emailQueue);
            //    }
            //});
        }
    }
}
