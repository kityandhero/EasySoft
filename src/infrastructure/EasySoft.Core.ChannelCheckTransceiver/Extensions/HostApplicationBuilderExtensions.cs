using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.ChannelCheckTransceiver.Assists;
using EasySoft.Core.ChannelCheckTransceiver.Consumers;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.ChannelCheckTransceiver.Extensions;

/// <summary>
/// HostApplicationBuilderExtensions
/// </summary>
public static class HostApplicationBuilderExtensions
{
    private const string IdentifierAddChannelCheckRabbitMQTransceiver = "da590e2881a64a4396314e5e172ea531";

    private const string IdentifierAddChannelCheckRabbitMQOnlyProducer = "e16831eb1bdc4a38b8b8f44a6f28729d";

    private const string IdentifierAddChannelCheckRabbitMQOnlyConsumer = "20d08b3ac982449d87fe2607c9b4955b";

    /// <summary>
    /// 装配一般日志 RabbitMQ 通道收发器
    /// </summary>
    /// <remarks>
    /// 同时发送和消费，使用场景较低，单端消费, 不应和单发送器和单消费器同时装配
    /// </remarks>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddChannelCheckRabbitMQTransceiver<TChannelCheckConsumer>(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    ) where TChannelCheckConsumer : BaseChannelCheckConsumer
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQTransceiver))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddChannelCheckRabbitMQTransceiver)}");

        var useTransmissionConfigValue = ChannelCheckTransceiverConfigureAssist.GetUseTransmission(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        if (!useTransmissionConfigValue.Value)
        {
            throw new ConfigErrorException(
                useTransmissionConfigValue.ToErrorDescription("开启消费者需要设置为true")
            );
        }

        builder.Services.AddChannelCheckTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQProducer(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQConsumer<TChannelCheckConsumer>(builder.Configuration, 1);

        builder.Services.AddChannelCheckCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddChannelCheckRabbitMQOnlyProducer(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddChannelCheckRabbitMQOnlyProducer)}");

        var useTransmissionConfigValue = ChannelCheckTransceiverConfigureAssist.GetUseTransmission(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        builder.Services.AddChannelCheckTransceiverConfigureAssist(builder.Configuration);

        if (useTransmissionConfigValue.Value)
        {
            builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
            builder.Services.AddChannelCheckRabbitMQProducer(builder.Configuration);
        }

        builder.Services.AddChannelCheckCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddChannelCheckRabbitMQOnlyConsumer<TChannelCheckConsumer>(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null,
        int instances = 1
    ) where TChannelCheckConsumer : BaseChannelCheckConsumer
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddChannelCheckRabbitMQOnlyConsumer)}");

        var useTransmissionConfigValue = ChannelCheckTransceiverConfigureAssist.GetUseTransmission(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        if (!useTransmissionConfigValue.Value)
        {
            throw new ConfigErrorException(
                useTransmissionConfigValue.ToErrorDescription("开启消费者需要设置为true")
            );
        }

        builder.Services.AddChannelCheckTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQConsumer<TChannelCheckConsumer>(builder.Configuration, instances);

        builder.Services.AddChannelCheckCollector(action);

        return builder;
    }
}