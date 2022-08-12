using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace NTech.WebAPI.Worker.EmailSend
{
    public class EmailSendWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IMessageConsumer _messageConsumer;
        public EmailSendWorker(IConfiguration configuration, IMessageConsumer messageConsumer)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            _messageConsumer = messageConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_messageConsumer.GetQueue((message) =>
            //{
            //    Debug.WriteLine(message);
            //});
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
                    Debug.WriteLine(message);
                };

                channel.BasicConsume(
                    queue: "NTechQueue",
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}