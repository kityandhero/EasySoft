using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace EasySoft.Core.Ocelot.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvanceOcelot(
        this IServiceCollection serviceCollection,
        bool useOcelotConfigFile = true
    )
    {
        if (useOcelotConfigFile)
        {
            serviceCollection.AddAdvanceOcelot(OcelotConfigAssist.GetConfiguration());
        }
        else
        {
            serviceCollection.AddOcelot()
                .AddPolly()
                .AddConsul()
                .AddConfigStoredInConsul();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage("Ocelot config use the default way.")
            );
        }

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseOcelot().Wait(); })
        );

        return serviceCollection;
    }

    private static IServiceCollection AddAdvanceOcelot(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddOcelot(configuration)
            .AddPolly()
            .AddConsul()
            .AddConfigStoredInConsul();

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage("Ocelot config use the ocelotConfig.json.")
                .SetExtra(OcelotConfigAssist.GetConfigFileInfo())
        );

        return serviceCollection;
    }
}