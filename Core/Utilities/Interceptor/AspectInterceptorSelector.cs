using Castle.DynamicProxy;
using Core.Aspect.Autofac.Exception;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using System.Reflection;

namespace Core.Utilities.Interceptor
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes
                <MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);

            classAttributes.Add(new LogAspect(typeof(FileLogger)));//info log
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //exception log
            classAttributes.Add(new PerformanceAspect(5)); //executing seconds > 5 send email to admin

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
