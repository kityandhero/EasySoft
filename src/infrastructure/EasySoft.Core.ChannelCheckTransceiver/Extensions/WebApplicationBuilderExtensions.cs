using EasySoft.Core.ChannelCheckTransceiver.Consumers;

namespace EasySoft.Core.ChannelCheckTransceiver.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
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
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddChannelCheckRabbitMQTransceiver<TChannelCheckConsumer>(
        this WebApplicationBuilder builder
    ) where TChannelCheckConsumer : BaseChannelCheckConsumer
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQTransceiver))
        {
            return builder;
        }

        builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQProducer(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQConsumer<TChannelCheckConsumer>(builder.Configuration, 1);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddChannelCheckRabbitMQOnlyProducer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQOnlyProducer))
        {
            return builder;
        }

        builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQProducer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 仅装配一般日志 RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddChannelCheckRabbitMQOnlyConsumer<TChannelCheckConsumer>(
        this WebApplicationBuilder builder,
        int instances = 1
    ) where TChannelCheckConsumer : BaseChannelCheckConsumer
    {
        if (builder.HasRegistered(IdentifierAddChannelCheckRabbitMQOnlyConsumer))
        {
            return builder;
        }

        builder.Services.AddChannelCheckRabbitMQCore(builder.Configuration);
        builder.Services.AddChannelCheckRabbitMQConsumer<TChannelCheckConsumer>(builder.Configuration, instances);

        return builder;
    }
}