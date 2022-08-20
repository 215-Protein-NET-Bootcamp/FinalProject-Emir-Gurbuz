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

namespace Core.Aspect.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogAspect(Type loggingtype)
        {
            WrongLoggingTypeException.ThrowIfNotEqual(typeof(LoggerServiceBase), loggingtype);

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingtype);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(getMethodDetail(invocation));
        }

        private string getMethodDetail(IInvocation invocation)
        {
            List<LogParameter> logParameters = invocation.Arguments.Select((a, i) => new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Value = a.CheckPasswordProperty() ? "***" : a?.GetType().ToString(),
                Type = a?.GetType().ToString()
            }).ToList();

            string email = _httpContextAccessor.HttpContext?.User?.ClaimEmail();
            List<string> roles = _httpContextAccessor.HttpContext?.User?.ClaimRoles();
            LogDetail logDetail = new()
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                Email = email,
                Roles = roles
            };
            return JsonConvert.SerializeObject(logDetail);
        }
    }
}
