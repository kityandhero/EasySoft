using EasySoft.Core.GeneralLogTransmitter.Producers;

namespace EasySoft.Core.GeneralLogTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddGeneralLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<GeneralLogProducer>().As<IGeneralLogProducer>().SingleInstance();
        });

        return builder;
    }
}