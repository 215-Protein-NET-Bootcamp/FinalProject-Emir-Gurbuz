using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            #region HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Cache
            //services.AddMemoryCache();
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            #endregion
        }
    }
}
