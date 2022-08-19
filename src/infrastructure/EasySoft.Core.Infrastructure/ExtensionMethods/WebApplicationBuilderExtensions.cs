using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.CommonAssists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseStaticFileOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : StaticFileOptions
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<StaticFileOptions>().SingleInstance();
        });

        return builder;
    }

    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<StaticFileOptions>())
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .Resolve<StaticFileOptions>();

            application.UseStaticFiles(staticFileOptions);
        }

        return application;
    }

    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="channel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplicationBuilder UseAdvanceApplicationChannel(
        this WebApplicationBuilder builder,
        int channel
    )
    {
        if (FlagAssist.ApplicationChannelInjectionComplete)
        {
            throw new Exception("UseAdvanceApplicationChannel disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterInstance(new ApplicationChannel().SetChannel(channel))
                .As<IApplicationChannel>().SingleInstance();
        });

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = false;

        return builder;
    }

    public static WebApplicationBuilder UseAdvanceDefaultApplicationChannel(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ApplicationChannel>().As<IApplicationChannel>().SingleInstance();
        });

        FlagAssist.ApplicationChannelInjectionComplete = true;
        FlagAssist.ApplicationChannelIsDefault = true;

        return builder;
    }
}