﻿using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Web;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder
    )
    {
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
            {
                LogManager.Configuration = new NLogLoggingConfiguration(configurationSection);
            }
            else
            {
                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder().AddJsonContent(
                        Tools.GetNlogDefaultConfig()
                    ).Build().GetSection("NLog")
                );
            }

            b.AddNLogWeb();
        });

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder,
        Func<LoggingConfiguration> action
    )
    {
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

        return builder;
    }
}