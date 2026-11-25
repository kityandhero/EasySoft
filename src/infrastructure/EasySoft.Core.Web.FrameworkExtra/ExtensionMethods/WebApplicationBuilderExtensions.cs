using EasySoft.Core.AdvanceNLog.Assists;
using EasySoft.Core.AdvanceNLog.Utils;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Result.Implements;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace EasySoft.Core.Web.FrameworkExtra.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    // private static WebApplicationBuilder AddConfigCenter(
    //     this WebApplicationBuilder builder,
    //     IChannel channel
    // )
    // {
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddConfigCenter)}."
    //     );
    //
    //     if (GeneralConfigAssist.GetConfigCenterSwitch())
    //     {
    //         StartupConfigMessageAssist.AddConfig(
    //             "ConfigCenterSwitch: enable.",
    //             GeneralConfigAssist.GetConfigFileInfo()
    //         );
    //
    //         StartupConfigMessageAssist.AddConfig(
    //             $"ConfigCenterType: {GeneralConfigAssist.GetConfigCenterType()}.",
    //             GeneralConfigAssist.GetConfigFileInfo()
    //         );
    //
    //         StartupDescriptionMessageAssist.AddPrompt(
    //             $"Dynamic config key is {Config.ConstCollection.GetDynamicConfigKeyCollection().Join(", ")}, they can set in ConfigCenter."
    //         );
    //
    //         if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.AgileConfig)
    //         {
    //             builder.AddAgileConfigClient(_ =>
    //                 {
    //                     var result = DynamicConfigAssist.GetNLogJsonConfig();
    //
    //                     LoadDynamicNLogJsonConfig(result);
    //                 }
    //             );
    //
    //             builder.AddAdvanceNLog(BuildDefaultConfig);
    //         }
    //         else if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.Consul)
    //         {
    //             builder.AddAdvanceConsulConfigCenter(
    //                 channel,
    //                 _ =>
    //                 {
    //                     var result = DynamicConfigAssist.GetNLogJsonConfig();
    //
    //                     LoadDynamicNLogJsonConfig(result);
    //                 }
    //             );
    //
    //             builder.AddAdvanceNLog(BuildDefaultConfig);
    //         }
    //         else
    //         {
    //             throw new Exception("Unknown config center.");
    //         }
    //     }
    //     else
    //     {
    //         StartupConfigMessageAssist.AddConfig(
    //             "ConfigCenterSwitch: disable.",
    //             GeneralConfigAssist.GetConfigFileInfo()
    //         );
    //
    //         builder.AddAdvanceNLog();
    //     }
    //
    //     return builder;
    // }

    // public static WebApplicationBuilder AddGateway(
    //     this WebApplicationBuilder builder
    // )
    // {
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddGateway)}."
    //     );
    //
    //     if (!GeneralConfigAssist.GetGatewaySwitch())
    //     {
    //         StartupConfigMessageAssist.AddConfig(
    //             "GatewaySwitch: disable.",
    //             GeneralConfigAssist.GetConfigFileInfo()
    //         );
    //
    //         return builder;
    //     }
    //
    //     StartupConfigMessageAssist.AddConfig(
    //         "GatewaySwitch: enable.",
    //         GeneralConfigAssist.GetConfigFileInfo()
    //     );
    //
    //     StartupConfigMessageAssist.AddConfig(
    //         $"GatewayType: {GeneralConfigAssist.GetGatewayType()}."
    //     );
    //
    //     if (GeneralConfigAssist.GetGatewayType() == GatewayType.Ocelot)
    //     {
    //         builder.AddAdvanceOcelot();
    //     }
    //     else
    //     {
    //         throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetGatewayType()}.");
    //     }
    //
    //     return builder;
    // }

    // private static WebApplicationBuilder AddRegistrationCenter(
    //     this WebApplicationBuilder builder
    // )
    // {
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddRegistrationCenter)}."
    //     );
    //
    //     if (!GeneralConfigAssist.GetRegistrationCenterSwitch())
    //     {
    //         StartupConfigMessageAssist.AddConfig(
    //             "RegistrationCenterSwitch: disable.",
    //             GeneralConfigAssist.GetConfigFileInfo()
    //         );
    //
    //         return builder;
    //     }
    //
    //     StartupConfigMessageAssist.AddConfig(
    //         "RegistrationCenterSwitch: enable.",
    //         GeneralConfigAssist.GetConfigFileInfo()
    //     );
    //
    //     StartupConfigMessageAssist.AddConfig(
    //         $"RegistrationCenterType: {GeneralConfigAssist.GetRegistrationCenterType()}.",
    //         GeneralConfigAssist.GetConfigFileInfo()
    //     );
    //
    //     if (GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
    //     {
    //         builder.AddAdvanceConsulRegistrationCenter();
    //     }
    //     else
    //     {
    //         throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetRegistrationCenterType()}.");
    //     }
    //
    //     return builder;
    // }

    private static void LoadDynamicNLogJsonConfig(
        ExecutiveResult<string> executiveResult,
        IConfiguration configuration,
        bool whetherDevelop
    )
    {
        if (!executiveResult.Success || (executiveResult.Success && string.IsNullOrWhiteSpace(executiveResult.Data)))
        {
            LogManager.Configuration = BuildDefaultConfig(configuration, whetherDevelop);

            LogManager.Configuration.Reload();

            return;
        }

        if (!string.IsNullOrWhiteSpace(executiveResult.Data))
        {
            var hasChanged = LogManagerAssist.CheckChange(executiveResult.Data);

            if (!hasChanged)
            {
                return;
            }

            try
            {
                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder().AddJsonContent(
                            executiveResult.Data
                        )
                        .Build()
                        .GetSection("NLog")
                );

                LogManager.Configuration.Reload();

                LoggerAssist.Info(
                    "Receive agileConfig changed transitiveMessage, nlog configure reload success."
                );
            }
            catch (Exception e)
            {
                LoggerAssist.Error(
                    $"some error occur, transitiveMessage: \"{e.Message}\", will use local config."
                );

                LogManager.Configuration = BuildDefaultConfig(configuration, whetherDevelop);

                LogManager.Configuration.Reload();
            }
        }
        else
        {
            LogManager.Configuration = BuildDefaultConfig(configuration, whetherDevelop);

            LogManager.Configuration.Reload();
        }
    }

    private static NLogLoggingConfiguration BuildDefaultConfig(
        IConfiguration cfg,
        bool whetherDevelop
    )
    {
        NLogLoggingConfiguration configuration;

        var configurationSection = LogConfigAssist.GetSection("NLog");

        if (configurationSection.GetChildren().Any())
        {
            configuration = new NLogLoggingConfiguration(configurationSection);
        }
        else
        {
            configuration = new NLogLoggingConfiguration(
                new ConfigurationBuilder().AddJsonContent(
                        Tools.GetNlogEmbedConfig(cfg, whetherDevelop)
                    )
                    .Build()
                    .GetSection("NLog")
            );
        }

        return configuration;
    }
}