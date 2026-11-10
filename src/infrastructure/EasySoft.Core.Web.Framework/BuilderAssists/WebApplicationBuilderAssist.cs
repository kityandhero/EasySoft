using EasySoft.Core.Web.Framework.ExtensionMethods;
using IChannel = EasySoft.UtilityTools.Standard.Interfaces.IChannel;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(
        string[] args
    )
    {
        return CreateBuilder<DefaultStartUpConfigure>(args);
    }

    public static WebApplicationBuilder CreateBuilder<TStartUpConfigure>(
        string[] args
    ) where TStartUpConfigure : IStartUpConfigure
    {
        var builder = CreateBuilder<TStartUpConfigure>(
            Channel.Unknown,
            args
        );

        return builder;
    }

    public static WebApplicationBuilder CreateBuilder(
        IChannel channel,
        string[] args
    )
    {
        return CreateBuilder<DefaultStartUpConfigure>(channel, args);
    }

    public static WebApplicationBuilder CreateBuilder<TStartUpConfigure>(
        IChannel channel,
        string[] args
    ) where TStartUpConfigure : IStartUpConfigure
    {
        var builder = WebApplication.CreateBuilder(
            new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory
            }
        );

        EnvironmentAssist.SetEnvironment(builder.Environment);

        // GeneralConfigAssist.Init();

        StartUpConfigureInit<TStartUpConfigure>();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(CreateBuilder)}<{typeof(TStartUpConfigure).Name}>."
        );

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddMultiJsonFile("appsettings.json")
                    .AddEnvironmentVariables();
            }
        );

        return CreateCore(builder, channel);
    }

    private static void StartUpConfigureInit<TStartUpConfigure>() where TStartUpConfigure : IStartUpConfigure
    {
        typeof(TStartUpConfigure).Create<IStartUpConfigure>().Init();

        var startUpConfigureInfo = $"{typeof(TStartUpConfigure).Name}.{nameof(IStartUpConfigure.Init)}";

        StartupDescriptionMessageAssist.AddExecute(
            startUpConfigureInfo
        );
    }

    private static WebApplicationBuilder CreateCore(
        WebApplicationBuilder builder,
        IChannel channel
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(CreateCore)}."
        );

        if (!string.IsNullOrWhiteSpace(EnvironmentAssist.GetEnvironmentAliasName()))
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "Current loading custom config file both with normal and special env, like generalConfig.json and generalConfig.dev.json."
            );
        }

        builder.AddAdvanceUrls();

        // builder.AddAdvanceAutoFac();
        // builder.AddCovertInjection();

        builder.AddAdvanceMapster();

        builder.AddAdvanceApplicationChannel(channel);

        // builder.AddRegistrationCenter();

        builder.AddAdvanceNLog();

        // builder.AddConfigCenter(channel);
        // builder.AddGateway();

        return builder;
    }
}