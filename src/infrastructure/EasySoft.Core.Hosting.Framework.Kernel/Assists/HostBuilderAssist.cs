using EasySoft.Core.AdvanceNLog.ExtensionMethods;
using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.Data.ExtensionMethods;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.MediatR.ExtensionMethods;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.Core.MiniProfiler.Extensions;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Configures;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.Hosting.Framework.Kernel.Assists;

public static class HostBuilderAssist
{
    public static IHostBuilder CreateBuilder(
        string[] args
    )
    {
        return CreateBuilder<DefaultStartUpConfigure>(args);
    }

    public static IHostBuilder CreateBuilder<TStartUpConfigure>(
        string[] args
    ) where TStartUpConfigure : IStartUpConfigure
    {
        var builder = CreateBuilder<TStartUpConfigure>(
            Channel.Unknown,
            args
        );

        return builder;
    }

    public static IHostBuilder CreateBuilder(
        IChannel channel,
        string[] args
    )
    {
        return CreateBuilder<DefaultStartUpConfigure>(channel, args);
    }

    public static IHostBuilder CreateBuilder<TStartUpConfigure>(
        IChannel channel,
        string[] args
    ) where TStartUpConfigure : IStartUpConfigure
    {
        var builder = Host.CreateDefaultBuilder(args);

        StartUpConfigureInit<TStartUpConfigure>();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(CreateBuilder)}<{typeof(TStartUpConfigure).Name}>."
        );

        return CreateCore(builder, channel);
    }

    private static IHostBuilder CreateCore(
        IHostBuilder builder,
        IChannel channel
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(CreateCore)}."
        );

        if (!string.IsNullOrWhiteSpace(HostEnvironmentAssist.GetEnvironmentAliasName()))
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "Current loading custom config file both with normal and special env, like simpleConfig.json and simpleConfig.dev.json."
            );
        }

        builder.ConfigureHostConfiguration(_ =>
                {
                    // 这里配置源已经加载好，包含 DOTNET_ENVIRONMENT
                }
            )

            // .ConfigureLogging(logging =>
            //     {
            //         logging.ClearProviders();
            //         logging.AddNLog();
            //     }
            // )
            .ConfigureServices((hostBuilderContext, services) =>
                {
                    HostEnvironmentAssist.SetEnvironment(hostBuilderContext.HostingEnvironment);

                    services.AddApplicationConfigureAssist(hostBuilderContext.Configuration);

                    services.AddJsonSerializerSettings();

                    services.AddSingleton<IHostEnvironment, HostingEnvironment>();

                    services.AddAdvanceNLog(
                        hostBuilderContext.Configuration,
                        HostEnvironmentAssist.IsDevelopment()
                    );

                    services.AddAdvanceApplicationChannel(channel);

                    services.AddHttpClient();

                    services.AddEndpointsApiExplorer();

                    #region Cache

                    services.AddCacheCore(hostBuilderContext.Configuration);

                    services.AddAdvanceCacheImplementation();

                    #endregion

                    services.AddAdvanceMapster();

                    services.AddAdvanceMediatR();

                    services.AddAdvanceMiniProfile(hostBuilderContext.Configuration);

                    services.AddKernelMessagingWithJson();

                    services.AddAssemblyBusinessServices();
                }
            );

        return builder;
    }

    private static void StartUpConfigureInit<TStartUpConfigure>() where TStartUpConfigure : IStartUpConfigure
    {
        typeof(TStartUpConfigure).Create<IStartUpConfigure>().Init();

        var startUpConfigureInfo = $"{typeof(TStartUpConfigure).Name}.{nameof(IStartUpConfigure.Init)}";

        StartupDescriptionMessageAssist.AddExecute(
            startUpConfigureInfo
        );
    }
}