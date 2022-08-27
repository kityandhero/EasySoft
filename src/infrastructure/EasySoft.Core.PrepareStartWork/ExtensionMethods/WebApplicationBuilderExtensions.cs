using Autofac;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.Core.PrepareStartWork.PrepareWorks;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseServerAddressesFeature(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ServerAddressesFeature>().As<IServerAddressesFeature>().SingleInstance();
        });

        return builder;
    }

    public static WebApplicationBuilder UseAdvanceUrls(
        this WebApplicationBuilder builder
    )
    {
        var urls = GeneralConfigAssist.GetUrls();

        if (string.IsNullOrWhiteSpace(urls))
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Warning,
                Message =
                    "Urls in generalConfig.json has not setting, suggest setting it with number or url, there will be better development experience."
            });

            return builder;
        }

        var urlsAdjust = urls.IsInt() ? $"http://localhost:{urls}" : urls;

        FlagAssist.StartupUrls = urlsAdjust;

        builder.WebHost.UseUrls(urlsAdjust);

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message = $"Startup urls is {urlsAdjust}."
        });

        return builder;
    }

    /// <summary>
    /// 注入框架的启动预处理任务，启动时自动执行, 仅允许配置一次
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseCovertInjection(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.CovertInjectionComplete)
        {
            throw new Exception("UseCovertInjection disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<PrepareCovertStartWork>().As<IPrepareCovertStartWork>().SingleInstance();
        });

        FlagAssist.CovertInjectionComplete = true;

        return builder;
    }

    /// <summary>
    /// 注入定制的启动预处理任务，注入任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UsePrepareStartWorkInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPrepareStartWork
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<IPrepareStartWork>().SingleInstance();
        });

        return builder;
    }
}