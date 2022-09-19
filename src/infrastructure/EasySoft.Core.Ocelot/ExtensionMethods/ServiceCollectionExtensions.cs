using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace EasySoft.Core.Ocelot.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    const string WarnMessage =
        "Ocelot stored default  key is \"InternalConfiguration\" (you can custom it by \"GlobalConfiguration\" -> \"ServiceDiscoveryProvider\" -> \"ConfigurationKey\"), please ensure that the key and value exists in Consul, if it not exist, will occur exception.";

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
            var ocelotBuilder = serviceCollection.AddOcelot()
                .AddPolly();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage("Ocelot config use the default way.")
            );

            if (GeneralConfigAssist.GetRegistrationCenterSwitch() &&
                GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
            {
                ocelotBuilder.AddConsul().AddConfigStoredInConsul();

                StartupDescriptionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Warning)
                        .SetMessage(WarnMessage)
                );
            }
            else
            {
                StartupDescriptionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            "You can use registration center (like Consul) with Ocelot to provides easier administration.")
                );
            }
        }

        return serviceCollection;
    }

    private static IServiceCollection AddAdvanceOcelot(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        var ocelotBuilder = serviceCollection.AddOcelot(configuration)
            .AddPolly();

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage("Ocelot config use the ocelotConfig.json.")
                .SetExtra(OcelotConfigAssist.GetConfigFileInfo())
        );

        if (GeneralConfigAssist.GetRegistrationCenterSwitch() &&
            GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
        {
            ocelotBuilder.AddConsul().AddConfigStoredInConsul();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Warning)
                    .SetMessage(WarnMessage)
            );
        }
        else
        {
            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "You can use registration center (like Consul) with Ocelot to provides easier administration.")
            );
        }

        return serviceCollection;
    }
}