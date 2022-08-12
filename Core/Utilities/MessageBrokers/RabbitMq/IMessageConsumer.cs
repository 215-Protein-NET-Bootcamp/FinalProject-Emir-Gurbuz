namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public interface IMessageConsumer
    {
        void GetQueue(Action<string> action);
    }
}
