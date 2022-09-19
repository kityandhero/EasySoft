using EasySoft.Core.AgileConfigClient.ExtensionMethods;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;
using EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;
using EasySoft.Core.DynamicConfig.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.NLog.Assists;
using EasySoft.Core.NLog.ExtensionMethods;
using EasySoft.Core.Ocelot.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(
        string[] args
    )
    {
        EnvironmentConfigAssist.Init();
        GeneralConfigAssist.Init();

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            ContentRootPath = AppContext.BaseDirectory
        });

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddMultiJsonFile("appsettings.json")
                .AddEnvironmentVariables();
        });

        return CreateCore(builder, new ApplicationChannel().SetChannel(0).SetName("默认应用"));
    }

    public static WebApplicationBuilder CreateBuilder(
        IApplicationChannel applicationChannel,
        string[] args
    )
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args
        });

        return CreateCore(builder, applicationChannel);
    }

    private static WebApplicationBuilder CreateCore(
        WebApplicationBuilder builder,
        IApplicationChannel applicationChannel
    )
    {
        StartupConfigMessageAssist.Add(
            new StartupMessage().SetLevel(LogLevel.Information)
                .SetMessage(
                    $"CustomEnv: {(string.IsNullOrWhiteSpace(EnvironmentConfigAssist.GetCustomEnv()) ? "not set" : EnvironmentConfigAssist.GetCustomEnv())}"
                )
        );

        if (!string.IsNullOrWhiteSpace(EnvironmentConfigAssist.GetCustomEnv()))
        {
            StartupDescriptionMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information)
                    .SetMessage(
                        "Current loading custom config is normal file and it with current customEnv file, like generalConfig.json and generalConfig.dev.json."
                    )
            );
        }

        var list = ApplicationConfigurator.GetAllEnv();

        if (list.Any())
        {
            StartupDescriptionMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"CustomEnv current available list: {list.Join(",")}, it can be adjusted by ApplicationConfigurator."
                    )
            );
        }

        builder.AddAdvanceUrls()
            .AddAdvanceAutoFac()
            .AddCovertInjection()
            .AddAdvanceMapster();

        builder.AddAdvanceApplicationChannel(applicationChannel);

        builder.BuildRegistrationCenter();
        builder.BuildConfigCenter(applicationChannel);
        builder.BuildGateway();

        return builder;
    }

    private static WebApplicationBuilder BuildRegistrationCenter(
        this WebApplicationBuilder builder
    )
    {
        if (!GeneralConfigAssist.GetRegistrationCenterSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "RegistrationCenterSwitch: disable."
                    )
            );

            return builder;
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    "RegistrationCenterSwitch: enable."
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"RegistrationCenterType: {GeneralConfigAssist.GetRegistrationCenterType()}."
                )
        );

        if (GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
        {
            builder.AddAdvanceConsulRegistrationCenter();
        }
        else
        {
            throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetRegistrationCenterType()}.");
        }

        return builder;
    }

    private static WebApplicationBuilder BuildConfigCenter(
        this WebApplicationBuilder builder,
        IApplicationChannel applicationChannel
    )
    {
        if (GeneralConfigAssist.GetConfigCenterSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "ConfigCenterSwitch: enable."
                    )
            );

            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"ConfigCenterType: {GeneralConfigAssist.GetConfigCenterType()}."
                    )
            );

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"Dynamic config key: {Config.ConstCollection.GetDynamicConfigKeyCollection().Join(",")}, they can set in ConfigCenter."
                    )
            );

            if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.AgileConfig)
            {
                builder.AddAgileConfigClient(_ =>
                {
                    var result = DynamicConfigAssist.GetNLogJsonConfig();

                    LoadDynamicNLogJsonConfig(result);
                });

                builder.AddAdvanceNLog(BuildDefaultConfig);
            }
            else if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.Consul)
            {
                builder.AddAdvanceConsulConfigCenter(
                    applicationChannel,
                    _ =>
                    {
                        var result = DynamicConfigAssist.GetNLogJsonConfig();

                        LoadDynamicNLogJsonConfig(result);
                    }
                );

                builder.AddAdvanceNLog(BuildDefaultConfig);
            }
            else
            {
                throw new Exception("Unknown config center.");
            }
        }
        else
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "ConfigCenterSwitch: disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );

            builder.AddAdvanceNLog();
        }

        return builder;
    }

    private static WebApplicationBuilder BuildGateway(
        this WebApplicationBuilder builder
    )
    {
        if (!GeneralConfigAssist.GetGatewaySwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "GatewaySwitch: disable."
                    )
            );

            return builder;
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    "GatewaySwitch: enable."
                )
        );

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"GatewayType: {GeneralConfigAssist.GetGatewayType()}."
                )
        );

        if (GeneralConfigAssist.GetGatewayType() == GatewayType.Ocelot)
        {
            builder.AddAdvanceOcelot();
        }
        else
        {
            throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetGatewayType()}.");
        }

        return builder;
    }

    private static void LoadDynamicNLogJsonConfig(ExecutiveResult<string> executiveResult)
    {
        if (!executiveResult.Success || (executiveResult.Success && string.IsNullOrWhiteSpace(executiveResult.Data)))
        {
            LogManager.Configuration = BuildDefaultConfig();

            LogManager.Configuration.Reload();

            return;
        }

        if (!string.IsNullOrWhiteSpace(executiveResult.Data))
        {
            var hasChanged = NLogAssist.CheckChange(executiveResult.Data);

            if (!hasChanged)
            {
                return;
            }

            try
            {
                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder().AddJsonContent(
                        executiveResult.Data
                    ).Build().GetSection("NLog")
                );

                LogManager.Configuration.Reload();

                LogAssist.Info("Receive agileConfig changed message, nlog configure reload success.");
            }
            catch (Exception e)
            {
                LogAssist.Error($"some error occur, message: \"{e.Message}\", will use local config.");

                LogManager.Configuration = BuildDefaultConfig();

                LogManager.Configuration.Reload();
            }
        }
        else
        {
            LogManager.Configuration = BuildDefaultConfig();

            LogManager.Configuration.Reload();
        }
    }

    private static NLogLoggingConfiguration BuildDefaultConfig()
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
                    Tools.GetNlogDefaultConfig()
                ).Build().GetSection("NLog")
            );
        }

        return configuration;
    }
}