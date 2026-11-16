using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.NLog.Assists;
using EasySoft.Core.NLog.Utils;
using EasySoft.UtilityTools.Core.Extensions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceNLog = "8c1ba1e4-2745-47e6-8286-afe73de2b7ed";

    /// <summary>
    /// 注入日志框架
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceNLog))
        {
            return builder;
        }

        builder.Services.AddSingleton(new LogConfigureAssist(builder.Configuration));

        // NLog: Setup NLog for Dependency injection
        // builder.Logging.ClearProviders();

        // 使用如下库实现，更换可能不会读取自定义配置
        // NLog.Extensions.Hosting
        // NLog.Web.AspNetCore

        builder.Services.AddLogging(b =>
            {
                b.ClearProviders();

                var embedConfig = Tools.GetNlogEmbedConfig(builder.Configuration);

                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder().AddJsonContent(embedConfig)
                        .Build()
                        .GetSection("NLog")
                );

                b.AddNLogWeb();
            }
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapNLogInlayConfig(); })
        );

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder,
        Func<LoggingConfiguration> action
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceNLog))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceNLog)}."
        );

        // NLog: Setup NLog for Dependency injection
        builder.Logging.ClearProviders();

        // 使用如下库实现，更换可能不会读取自定义配置
        // NLog.Extensions.Hosting
        // NLog.Web.AspNetCore

        builder.Services.AddLogging(b =>
            {
                b.ClearProviders();

                LogManager.Configuration = action();

                b.AddNLogWeb();
            }
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapNLogInlayConfig(); })
        );

        return builder;
    }
}