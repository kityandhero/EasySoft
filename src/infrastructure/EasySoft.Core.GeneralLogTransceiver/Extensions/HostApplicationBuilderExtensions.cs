using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.GeneralLogTransceiver.Assists;
using EasySoft.Core.GeneralLogTransceiver.Consumers;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.GeneralLogTransceiver.Extensions;

/// <summary>
/// HostApplicationBuilderExtensions
/// </summary>
public static class HostApplicationBuilderExtensions
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
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddGeneralLogRabbitMQTransceiver<TGeneralLogConsumer>(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    ) where TGeneralLogConsumer : BaseGeneralLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQTransceiver))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddGeneralLogRabbitMQTransceiver)}");

        var useTransmissionConfigValue = GeneralLogTransceiverConfigureAssist.GetUseTransmission(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        if (!useTransmissionConfigValue.Value)
        {
            throw new ConfigErrorException(
                useTransmissionConfigValue.ToErrorDescription("开启消费者需要设置为true")
            );
        }

        builder.Services.AddGeneralLogTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQProducer(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQConsumer<TGeneralLogConsumer>(builder.Configuration, 1);

        builder.Services.AddGeneralLogCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddGeneralLogRabbitMQOnlyProducer(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddGeneralLogRabbitMQOnlyProducer)}");

        var useTransmissionConfigValue = GeneralLogTransceiverConfigureAssist.GetUseTransmission(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        builder.Services.AddGeneralLogTransceiverConfigureAssist(builder.Configuration);

        if (useTransmissionConfigValue.Value)
        {
            builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
            builder.Services.AddGeneralLogRabbitMQProducer(builder.Configuration);
        }

        builder.Services.AddGeneralLogCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddGeneralLogRabbitMQOnlyConsumer<TGeneralLogConsumer>(
        this IHostApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null,
        int instances = 1
    ) where TGeneralLogConsumer : BaseGeneralLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddGeneralLogRabbitMQOnlyConsumer)}");

        var useTransmissionConfigValue = GeneralLogTransceiverConfigureAssist.GetUseTransmission(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        if (!useTransmissionConfigValue.Value)
        {
            throw new ConfigErrorException(
                useTransmissionConfigValue.ToErrorDescription("开启消费者需要设置为true")
            );
        }

        builder.Services.AddGeneralLogTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);
        builder.Services.AddGeneralLogRabbitMQConsumer<TGeneralLogConsumer>(builder.Configuration, instances);

        builder.Services.AddGeneralLogCollector(action);

        return builder;
    }
}