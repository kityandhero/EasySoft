using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

            StartupDescriptionMessageAssist.AddPrompt(
                "Ocelot config use the ocelotConfig.json.",
                OcelotConfigAssist.GetConfigFileInfo()
            );
        }
        else
        {
            ocelotBuilder = serviceCollection.AddOcelot();

            StartupDescriptionMessageAssist.AddPrompt(
                "Ocelot config use the default way."
            );
        }

        ocelotBuilder = ocelotBuilder.AddPolly();

        if (GeneralConfigAssist.GetGatewayWithConsulSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "GatewayWithConsulSwitch: enable"
            );

            ocelotBuilder = ocelotBuilder.AddConsul();

            if (GeneralConfigAssist.GetGatewayConfigInConsulSwitch())
            {
                ocelotBuilder.AddConfigStoredInConsul();

                StartupConfigMessageAssist.AddConfig(
                    "GatewayConfigInConsulSwitch: enable"
                );
            }
            else
            {
                StartupConfigMessageAssist.AddConfig(
                    "GatewayConfigInConsulSwitch: disable"
                );
            }
        }
        else
        {
            StartupConfigMessageAssist.AddConfig(
                "GatewayWithConsulSwitch: disable"
            );

            StartupConfigMessageAssist.AddConfig(
                "GatewayConfigInConsulSwitch: disable"
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