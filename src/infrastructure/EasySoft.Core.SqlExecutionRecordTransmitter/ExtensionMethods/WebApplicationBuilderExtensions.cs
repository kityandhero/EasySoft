using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseGeneralLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<SqlExecutionRecordProducer>().As<ISqlExecutionRecordProducer>()
                .SingleInstance();
        });

        return builder;
    }
}