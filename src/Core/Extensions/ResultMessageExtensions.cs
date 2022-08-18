using Core.Exceptions.Extension;
using Core.Utilities.ResultMessage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ResultMessageExtensions
    {
        public static IServiceCollection AddMessageLanguage(this IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration.GetSection("MessageResultLanguage").Value)
            {
                case "Tr":
                    return services.AddMessageLanguage(typeof(TurkishMessageLanguage));

                default:
                    return services.AddMessageLanguage(typeof(EnglishMessageLanguage));
            }
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
