using Core.Utilities.Interceptor;
using Core.Utilities.IoC;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Entity.Concrete;
using Microsoft.Extensions.Configuration;
using Core.Enums;

namespace Core.Aspect.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly Stopwatch _stopwatch;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        private readonly IConfiguration _configuration;
        private readonly int _interval;
        public PerformanceAspect(int interval)
        {
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            _messageBrokerHelper = ServiceTool.ServiceProvider.GetService<IMessageBrokerHelper>();
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            _interval = interval;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string message = $"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} -> total seconds:{_stopwatch.Elapsed.TotalSeconds}";
                _messageBrokerHelper.QueueMessage(QueueNameEnum.EmailQueue.ToString(), new EmailQueue
                {
                    Email = _configuration.GetSection("AdminUser:Email").Value,
                    Subject = "Application Performance Alert",
                    Body = message,
                });
                Debug.WriteLine(message);
            }
            _stopwatch.Reset();
        }
    }
}
