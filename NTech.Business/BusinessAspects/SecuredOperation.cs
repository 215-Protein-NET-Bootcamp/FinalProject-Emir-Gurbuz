using Castle.DynamicProxy;
using Core.Exceptions.Middleware;
using Core.Extensions;
using Core.Utilities.Interceptor;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NTech.Business.BusinessAspects
{
    public class SecuredOperation : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string[] _roles;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            List<string> claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (string role in _roles)
                if (claimRoles.Contains(role))
                    return;
            throw new UnauthorizedException();
        }
    }
}
