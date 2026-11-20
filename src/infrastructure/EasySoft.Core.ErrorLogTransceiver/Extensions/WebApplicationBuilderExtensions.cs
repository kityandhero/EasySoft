using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.ErrorLogTransceiver.Assists;
using EasySoft.Core.ErrorLogTransceiver.Consumers;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;

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
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQTransceiver<TErrorLogConsumer>(
        this WebApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    ) where TErrorLogConsumer : BaseErrorLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQTransceiver))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddErrorLogRabbitMQTransceiver)}");

        var useTransmissionConfigValue = ErrorLogTransceiverConfigureAssist.GetUseTransmission(
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

        builder.Services.AddErrorLogTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQProducer(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQConsumer<TErrorLogConsumer>(builder.Configuration, 1);

        builder.Services.AddErrorLogCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配错误日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQOnlyProducer(
        this WebApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null
    )
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQOnlyProducer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddErrorLogRabbitMQOnlyProducer)}");

        var useTransmissionConfigValue = ErrorLogTransceiverConfigureAssist.GetUseTransmission(
            builder.Configuration
        );

        StartupConfigMessageAssist.AddConfig(
            useTransmissionConfigValue.ToArrowDescription()
        );

        builder.Services.AddErrorLogTransceiverConfigureAssist(builder.Configuration);

        if (useTransmissionConfigValue.Value)
        {
            builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
            builder.Services.AddErrorLogRabbitMQProducer(builder.Configuration);
        }

        builder.Services.AddErrorLogCollector(action);

        return builder;
    }

    /// <summary>
    /// 仅装配错误日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddErrorLogRabbitMQOnlyConsumer<TErrorLogConsumer>(
        this WebApplicationBuilder builder,
        Action<DateRollingUtf8FileRecorderOptions> action = null,
        int instances = 1
    ) where TErrorLogConsumer : BaseErrorLogConsumer
    {
        if (builder.HasRegistered(IdentifierAddErrorLogRabbitMQOnlyConsumer))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddErrorLogRabbitMQOnlyConsumer)}");

        var useTransmissionConfigValue = ErrorLogTransceiverConfigureAssist.GetUseTransmission(
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

        builder.Services.AddErrorLogTransceiverConfigureAssist(builder.Configuration);

        builder.Services.AddErrorLogRabbitMQCore(builder.Configuration);
        builder.Services.AddErrorLogRabbitMQConsumer<TErrorLogConsumer>(builder.Configuration, instances);

        builder.Services.AddErrorLogCollector(action);

        return builder;
    }
}