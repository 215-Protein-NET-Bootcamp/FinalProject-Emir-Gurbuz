using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptor;
using Core.Utilities.IoC;
using Core.Utilities.Result;
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

            //if return value ErrorResult when don't remove cache key
            dynamic task = invocation.ReturnValue as dynamic;
            task.Wait();
            if (task.Result is ErrorResult)
                return;

            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
