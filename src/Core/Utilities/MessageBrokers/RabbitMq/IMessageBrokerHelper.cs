namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public interface IMessageBrokerHelper
    {
        void QueueMessage(string queueName, object messageText);
    }
}
