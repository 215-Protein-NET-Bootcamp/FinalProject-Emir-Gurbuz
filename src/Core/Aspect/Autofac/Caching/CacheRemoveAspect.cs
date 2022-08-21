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
            //if return value ErrorResult when don't remove cache key
            dynamic returnValue = invocation.ReturnValue as dynamic;
            if (returnValue is Task)
            {
                returnValue.Wait();
                returnValue = returnValue.Result;
            }
            if (returnValue is ErrorResult)
                return;
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
