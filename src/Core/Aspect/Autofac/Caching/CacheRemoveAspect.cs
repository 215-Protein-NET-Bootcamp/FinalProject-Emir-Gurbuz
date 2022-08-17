using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptor;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspect.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly ICacheManager _cacheManager;
        private string _pattern;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            if (_pattern.StartsWith("*") == false && _pattern.EndsWith("*") == false)
                _pattern = $"*{_pattern}*";
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
