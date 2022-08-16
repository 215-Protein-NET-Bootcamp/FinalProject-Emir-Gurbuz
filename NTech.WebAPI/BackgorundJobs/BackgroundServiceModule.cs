using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using NTech.Business.Abstract;
using NTech.Business.Concrete;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.Concrete.EntityFramework;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.DataAccess.UnitOfWork.Concrete;

namespace NTech.WebAPI.BackgorundJobs
{
    public class BackgroundServiceModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IEmailConsumer, MqConsumerHelper>();
            services.AddSingleton<IMessageBrokerHelper, MqQueueHelper>();
            services.AddSingleton<IEmailSender, SmtpEmailSender>();

            services.AddScoped<IEmailQueueService, EmailQueueManager>();
            services.AddScoped<IEmailQueueDal, EfEmailQueueDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
