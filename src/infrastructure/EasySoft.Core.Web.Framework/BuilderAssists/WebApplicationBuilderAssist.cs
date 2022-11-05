using EasySoft.Core.AgileConfigClient.ExtensionMethods;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;
using EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;
using EasySoft.Core.DynamicConfig.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.NLog.Assists;
using EasySoft.Core.NLog.ExtensionMethods;
using EasySoft.Core.Gateway.Ocelot.ExtensionMethods;
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

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(
        string[] args
    )
    {
        return CreateBuilder(new ApplicationChannel().SetChannel(0).SetName("默认应用"), args);
    }

    public static WebApplicationBuilder CreateBuilder(
        IApplicationChannel applicationChannel,
        string[] args
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(CreateBuilder)}()."
        );

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            ContentRootPath = AppContext.BaseDirectory
        });

        EnvironmentAssist.SetEnvironment(builder.Environment);

        GeneralConfigAssist.Init();

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddMultiJsonFile("appsettings.json")
                .AddEnvironmentVariables();
        });

        return CreateCore(builder, applicationChannel);
    }

    private static WebApplicationBuilder CreateCore(
        WebApplicationBuilder builder,
        IApplicationChannel applicationChannel
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(CreateCore)}()."
        );

        StartupConfigMessageAssist.AddConfig(
            $"EnvironmentAlias: {EnvironmentAssist.GetEnvironmentAliasName()}."
        );

        if (!string.IsNullOrWhiteSpace(EnvironmentAssist.GetEnvironmentAliasName()))
            StartupDescriptionMessageAssist.AddDescription(
                "Current loading custom config file both with normal and special env, like generalConfig.json and generalConfig.dev.json."
            );

        builder.AddAdvanceUrls()
            .AddAdvanceAutoFac()
            .AddCovertInjection()
            .AddAdvanceMapster();

        builder.AddAdvanceApplicationChannel(applicationChannel);

        builder.AddRegistrationCenter();
        builder.AddConfigCenter(applicationChannel);
        builder.AddGateway();

        return builder;
    }

    private static WebApplicationBuilder AddRegistrationCenter(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddRegistrationCenter)}()."
        );

        if (!GeneralConfigAssist.GetRegistrationCenterSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "RegistrationCenterSwitch: disable."
            );

            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            "RegistrationCenterSwitch: enable."
        );

        StartupConfigMessageAssist.AddConfig(
            $"RegistrationCenterType: {GeneralConfigAssist.GetRegistrationCenterType()}."
        );

        if (GeneralConfigAssist.GetRegistrationCenterType() == RegistrationCenterType.Consul)
            builder.AddAdvanceConsulRegistrationCenter();
        else
            throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetRegistrationCenterType()}.");

        return builder;
    }

    private static WebApplicationBuilder AddConfigCenter(
        this WebApplicationBuilder builder,
        IApplicationChannel applicationChannel
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddConfigCenter)}()."
        );

        if (GeneralConfigAssist.GetConfigCenterSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "ConfigCenterSwitch: enable."
            );

            StartupConfigMessageAssist.AddConfig(
                $"ConfigCenterType: {GeneralConfigAssist.GetConfigCenterType()}."
            );

            StartupDescriptionMessageAssist.AddDescription(
                $"Dynamic config key: {Config.ConstCollection.GetDynamicConfigKeyCollection().Join(",")}, they can set in ConfigCenter."
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
            StartupConfigMessageAssist.AddConfig(
                "ConfigCenterSwitch: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            builder.AddAdvanceNLog();
        }

        return builder;
    }

    private static WebApplicationBuilder AddGateway(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddGateway)}()."
        );

        if (!GeneralConfigAssist.GetGatewaySwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "GatewaySwitch: disable."
            );

            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            "GatewaySwitch: enable."
        );

        StartupConfigMessageAssist.AddConfig(
            $"GatewayType: {GeneralConfigAssist.GetGatewayType()}."
        );

        if (GeneralConfigAssist.GetGatewayType() == GatewayType.Ocelot)
            builder.AddAdvanceOcelot();
        else
            throw new Exception($"Unknown registration center: {GeneralConfigAssist.GetGatewayType()}.");

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

            if (!hasChanged) return;

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
            configuration = new NLogLoggingConfiguration(configurationSection);
        else
            configuration = new NLogLoggingConfiguration(
                new ConfigurationBuilder().AddJsonContent(
                    Tools.GetNlogDefaultConfig()
                ).Build().GetSection("NLog")
            );

        return configuration;
    }
}