using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Exceptions.Aspect;
using Core.Utilities.Interceptor;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using System.Reflection;

namespace Core.Aspect.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionLogAspect(Type loggingType)
        {
            WrongLoggingTypeException.ThrowIfNotEqual(typeof(LoggerServiceBase), loggingType);

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingType);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
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
                Type = a?.GetType().ToString(),
                Value = a.CheckPasswordProperty() ? "***" : a?.GetType().ToString()
            }).ToList();

            string email = _httpContextAccessor.HttpContext.User.ClaimEmail();
            List<string> roles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            LogDetailWithException logDetailWithException = new LogDetailWithException
            {
                ExceptionMessage = e.Message,
                MethodName = invocation.Method.Name,
                Parameters = parameters,
                Email = email,
                Roles = roles
            };

            return JsonConvert.SerializeObject(logDetailWithException);
        }

    }
}
