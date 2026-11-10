using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.GeneralLogTransceiver.Consumers;
using EasySoft.Core.GeneralLogTransceiver.Producers;
using EasySoft.Core.Infrastructure.Assists;

namespace EasySoft.Core.GeneralLogTransceiver.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddGeneralLogRabbitMQTransceiver = "86f5bba438a741158aab45f6dfa5f3b5";

    private const string IdentifierAddGeneralLogRabbitMQOnlyProducer = "661de5f68186420881735a7764a92b8f";

    private const string IdentifierAddGeneralLogRabbitMQOnlyConsumer = "9d235199fbe84fe9afbfa30b609c1f5d";

    /// <summary>
    /// 装配一般日志 RabbitMQ 通道收发器
    /// </summary>
    /// <remarks>
    /// 同时发送和消费，使用场景较低，单端消费, 不应和单发送器和单消费器同时装配
    /// </remarks>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddGeneralLogRabbitMQTransceiver<TGeneralLogConsumer>(
        this WebApplicationBuilder builder
    ) where TGeneralLogConsumer : BaseGeneralLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQTransceiver))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQProducer(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQConsumer<TGeneralLogConsumer>(builder.Configuration, 1);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddGeneralLogRabbitMQOnlyProducer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQProducer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddGeneralLogRabbitMQOnlyConsumer<TGeneralLogConsumer>(
        this WebApplicationBuilder builder,
        int instances = 1
    ) where TGeneralLogConsumer : BaseGeneralLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQConsumer<TGeneralLogConsumer>(builder.Configuration, instances);

        return builder;
    }
}