﻿using EasySoft.Core.Infrastructure.Configures;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceNLog = "8c1ba1e4-2745-47e6-8286-afe73de2b7ed";

    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceNLog))
            return builder;

        // NLog: Setup NLog for Dependency injection
        builder.Logging.ClearProviders();

        // 使用如下库实现，更换可能不会读取自定义配置
        // NLog.Extensions.Hosting
        // NLog.Web.AspNetCore

        builder.Services.AddLogging(b =>
        {
            b.ClearProviders();

            var configurationSection = LogConfigAssist.GetSection("NLog");

            if (configurationSection.GetChildren().Any())
                LogManager.Configuration = new NLogLoggingConfiguration(configurationSection);
            else
                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder().AddJsonContent(
                        Tools.GetNlogEmbedConfig()
                    ).Build().GetSection("NLog")
                );

            b.AddNLogWeb();
        });

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
            return builder;

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
        });

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => { endpoints.MapNLogInlayConfig(); })
        );

        return builder;
    }
}