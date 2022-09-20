using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace EasySoft.Core.Gateway.Ocelot.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvanceOcelot(
        this IServiceCollection serviceCollection,
        bool useOcelotConfigFile = true
    )
    {
        IOcelotBuilder ocelotBuilder;

        if (useOcelotConfigFile)
        {
            ocelotBuilder = serviceCollection.AddOcelot(OcelotConfigAssist.GetConfiguration());

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage("Ocelot config use the ocelotConfig.json.")
                    .SetExtra(OcelotConfigAssist.GetConfigFileInfo())
            );
        }
        else
        {
            ocelotBuilder = serviceCollection.AddOcelot();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage("Ocelot config use the default way.")
            );
        }

        ocelotBuilder = ocelotBuilder.AddPolly();

        if (GeneralConfigAssist.GetGatewayWithConsulSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information)
                    .SetMessage("GatewayWithConsulSwitch: enable")
            );

            ocelotBuilder = ocelotBuilder.AddConsul();

            if (GeneralConfigAssist.GetGatewayConfigInConsulSwitch())
            {
                ocelotBuilder = ocelotBuilder.AddConfigStoredInConsul();

                StartupConfigMessageAssist.Add(
                    new StartupMessage().SetLevel(LogLevel.Information)
                        .SetMessage("GatewayConfigInConsulSwitch: enable")
                );
            }
            else
            {
                StartupConfigMessageAssist.Add(
                    new StartupMessage().SetLevel(LogLevel.Information)
                        .SetMessage("GatewayConfigInConsulSwitch: disable")
                );
            }
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information)
                    .SetMessage("GatewayWithConsulSwitch: disable")
            );

            StartupConfigMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information)
                    .SetMessage("GatewayConfigInConsulSwitch: disable")
            );
        }

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseOcelot().Wait(); })
        );

        return serviceCollection;
    }
}