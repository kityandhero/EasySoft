using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
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
            var ocelotBuilder = serviceCollection.AddOcelot()
                .AddPolly();

            if (GeneralConfigAssist.GetRegistrationCenterSwitch() ||
                GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
            {
                ocelotBuilder.AddConsul()
                    .AddConfigStoredInConsul();
            }
        }

        return serviceCollection;
    }

    internal static IServiceCollection AddAdvanceOcelot(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        var ocelotBuilder = serviceCollection.AddOcelot(configuration)
            .AddPolly();

        // if (FlagAssist.GetInitializeConfigWhetherComplete())
        if (GeneralConfigAssist.GetRegistrationCenterSwitch() ||
            GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
        {
            ocelotBuilder.AddConsul()
                .AddConfigStoredInConsul();
        }

        return serviceCollection;
    }
}