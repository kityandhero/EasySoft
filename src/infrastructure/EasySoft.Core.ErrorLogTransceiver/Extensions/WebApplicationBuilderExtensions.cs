using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ErrorLogTransceiver.Consumers;
using EasySoft.Core.ErrorLogTransceiver.Producers;
using EasySoft.Core.Infrastructure.Assists;

namespace EasySoft.Core.ErrorLogTransceiver.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddErrorLogRabbitMQTransceiver = "85fad15fba9a45d6a77cec63eb6554d8";

    private const string IdentifierAddErrorLogRabbitMQOnlyProducer = "6847598d500543ad90c679c43205eeca";

    private const string IdentifierAddErrorLogRabbitMQOnlyConsumer = "c3f6357c77ca4c92a2b4daed2fd26fab";

    /// <summary>
    /// 装配错误日志 RabbitMQ 通道收发器
    /// </summary>
    /// <remarks>
    /// 同时发送和消费，使用场景较低，单端消费, 不应和单发送器和单消费器同时装配
    /// </remarks>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQTransceiver<TErrorLogConsumer>(
        this WebApplicationBuilder builder
    ) where TErrorLogConsumer : BaseErrorLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQTransceiver))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteErrorLogSwitch()
                ? "RemoteErrorLogEnable: enable."
                : "RemoteErrorLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQProducer(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQConsumer<TErrorLogConsumer>(builder.Configuration, 1);

        return builder;
    }

    /// <summary>
    /// 仅装配错误日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQOnlyProducer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteErrorLogSwitch()
                ? "RemoteErrorLogEnable: enable."
                : "RemoteErrorLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQProducer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 仅装配错误日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQOnlyConsumer<TErrorLogConsumer>(
        this WebApplicationBuilder builder,
        int instances = 1
    ) where TErrorLogConsumer : BaseErrorLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteErrorLogSwitch()
                ? "RemoteErrorLogEnable: enable."
                : "RemoteErrorLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQConsumer<TErrorLogConsumer>(builder.Configuration, instances);

        return builder;
    }
}