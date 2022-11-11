using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAdvanceExceptionless(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddExceptionless(configuration =>
        {
            configuration.ServerUrl = GeneralConfigAssist.GetExceptionlessServerUrl();
            configuration.ApiKey = GeneralConfigAssist.GetExceptionlessApiKey();
        });

        return serviceCollection;
    }
}