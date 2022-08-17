using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, params ICoreModule[] modules)
        {
            foreach (ICoreModule module in modules)
                module.Load(services);

            return ServiceTool.Create(services);
        }
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services)
        {
            return ServiceTool.Create(services);
        }
    }
}
