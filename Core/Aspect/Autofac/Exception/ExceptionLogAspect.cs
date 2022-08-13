using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Exceptions.Aspect;
using Core.Utilities.Interceptor;
using Newtonsoft.Json;

namespace Core.Aspect.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        public ExceptionLogAspect(Type loggingType)
        {
            WrongLoggingTypeException.ThrowIfNotEqual(typeof(LoggerServiceBase), loggingType);

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingType);
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            _loggerServiceBase.Error(getMethodDetail(invocation, e));
        }

        private string getMethodDetail(IInvocation invocation, System.Exception e)
        {
            List<LogParameter> parameters = invocation.Arguments.Select((a, i) => new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Type = a.GetType().ToString(),
                Value = a
            }).ToList();

            LogDetailWithException logDetailWithException = new LogDetailWithException
            {
                ExceptionMessage = e.Message,
                MethodName = invocation.Method.Name,
                Parameters = parameters
            };

            return JsonConvert.SerializeObject(logDetailWithException);
        }
    }
}
