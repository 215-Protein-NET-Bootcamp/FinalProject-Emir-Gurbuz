﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public class MqConsumerHelper : IMessageConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        public MqConsumerHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
        }

        public void GetQueue(Action<string> action)
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

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, mq) =>
                {
                    await Task.Delay(1000);
                    await Task.Run(() =>
                    {
                        var body = mq.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        action(message);
                    });
                };

                channel.BasicConsume(
                    queue: "NTechQueue",
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}
