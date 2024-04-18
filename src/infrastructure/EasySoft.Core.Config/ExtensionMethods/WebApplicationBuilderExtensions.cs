using EasySoft.Core.Config.Options;

namespace EasySoft.Core.Config.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入定制的静态文件配置，诸如任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddStaticFileOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : AdvanceStaticFileOptions
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(
            containerBuilder => { containerBuilder.RegisterType<T>().As<AdvanceStaticFileOptions>().SingleInstance(); }
        );

        return builder;
    }
}