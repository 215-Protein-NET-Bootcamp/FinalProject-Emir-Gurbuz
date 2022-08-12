using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;

namespace NTech.WebAPI.BackgorundJobs
{
    public class BackgroundServiceModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<IMessageBrokerHelper, MqQueueHelper>();
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
    }
}
