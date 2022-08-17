using Core.Utilities.ResultMessage;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ResultMessageExtensions
    {
        public static IServiceCollection AddMessageLanguage(this IServiceCollection services, ILanguageMessage languageMessage)
        {
            services.AddSingleton<ILanguageMessage>(languageMessage);
            return services;
        }
    }
}
