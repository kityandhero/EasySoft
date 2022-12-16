using Autofac;
using EasySoft.Core.ErrorLogTransmitter.Producers;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddErrorLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ErrorLogProducer>().As<IErrorLogProducer>().SingleInstance();
        });

        return builder;
    }
}