using EasySoft.Core.Web.Framework.ExtensionMethods;
using NLog;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(
        string[] args
    )
    {
        StartupDescriptionMessageAssist.AddPrompt(
            "ApplicationChannel use default, suggest using CreateBuilder(IApplicationChannel applicationChannel,string[] args) with your Application, it make the data source easy to identify in the remote log."
        );

        var builder = CreateBuilder(
            new ApplicationChannel()
                .SetChannel(ApplicationChannel.DefaultChannel)
                .SetName(ApplicationChannel.DefaultName),
            args
        );

        return builder;
    }

    public static WebApplicationBuilder CreateBuilder(
        IApplicationChannel applicationChannel,
        string[] args
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(CreateBuilder)}."
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
            $"{nameof(CreateCore)}."
        );

        if (!string.IsNullOrWhiteSpace(EnvironmentAssist.GetEnvironmentAliasName()))
            StartupDescriptionMessageAssist.AddPrompt(
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
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddRegistrationCenter)}."
        );

        if (!GeneralConfigAssist.GetRegistrationCenterSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "RegistrationCenterSwitch: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            "RegistrationCenterSwitch: enable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        StartupConfigMessageAssist.AddConfig(
            $"RegistrationCenterType: {GeneralConfigAssist.GetRegistrationCenterType()}.",
            GeneralConfigAssist.GetConfigFileInfo()
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
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddConfigCenter)}."
        );

        if (GeneralConfigAssist.GetConfigCenterSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "ConfigCenterSwitch: enable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            StartupConfigMessageAssist.AddConfig(
                $"ConfigCenterType: {GeneralConfigAssist.GetConfigCenterType()}.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            StartupDescriptionMessageAssist.AddPrompt(
                $"Dynamic config key is {Config.ConstCollection.GetDynamicConfigKeyCollection().Join(", ")}, they can set in ConfigCenter."
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
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddGateway)}."
        );

        if (!GeneralConfigAssist.GetGatewaySwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "GatewaySwitch: disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            "GatewaySwitch: enable.",
            GeneralConfigAssist.GetConfigFileInfo()
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