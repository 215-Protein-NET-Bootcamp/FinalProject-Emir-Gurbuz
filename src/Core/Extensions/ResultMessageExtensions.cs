using Core.Exceptions.Extension;
using Core.Utilities.ResultMessage;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ResultMessageExtensions
    {
        public static IServiceCollection AddMessageLanguage(this IServiceCollection services, ILanguageMessage languageMessage)
        {
            WrongLanguageMessageTypeException.ThrowIfNotAssignable(languageMessage);

            services.AddSingleton<ILanguageMessage>(languageMessage);
            return services;
        }
        public static IServiceCollection AddMessageLanguage(this IServiceCollection services, Type languageMessageType)
        {
            WrongLanguageMessageTypeException.ThrowIfNotAssignable(languageMessageType);

            ILanguageMessage languageMessage = (ILanguageMessage)Activator.CreateInstance(languageMessageType);

            services.AddSingleton<ILanguageMessage>(languageMessage);
            return services;
        }
    }
}
