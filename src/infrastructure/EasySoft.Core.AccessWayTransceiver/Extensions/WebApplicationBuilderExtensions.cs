using EasySoft.Core.AccessWayTransceiver.Consumers;

namespace EasySoft.Core.AccessWayTransceiver.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddAccessWayRabbitMQTransceiver = "4ec18ad1548347c2a5f0cc73526c332d";

    private const string IdentifierAddAccessWayRabbitMQOnlyProducer = "e556428cbe0048d1a6f379b8bb2d9942";

    private const string IdentifierAddAccessWayRabbitMQOnlyConsumer = "167fe5bb859b47e8b4183cffa3d16afb";

    /// <summary>
    /// 装配 AccessWay RabbitMQ 通道收发器
    /// </summary>
    /// <remarks>
    /// 同时发送和消费，使用场景较低，单端消费, 不应和单发送器和单消费器同时装配
    /// </remarks>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAccessWayRabbitMQTransceiver<TAccessWayConsumer>(
        this WebApplicationBuilder builder
    ) where TAccessWayConsumer : BaseAccessWayConsumer
    {
        if (builder.HasRegistered(IdentifierAddAccessWayRabbitMQTransceiver))
        {
            return builder;
        }

        builder.Services.AddAccessWayRabbitMQCore(builder.Configuration);
        builder.Services.AddAccessWayRabbitMQProducer(builder.Configuration);
        builder.Services.AddAccessWayRabbitMQConsumer<TAccessWayConsumer>(builder.Configuration, 1);

        return builder;
    }

    /// <summary>
    /// 仅装配 AccessWay RabbitMQ 通道发送者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAccessWayRabbitMQOnlyProducer(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddAccessWayRabbitMQOnlyProducer))
        {
            return builder;
        }

        builder.Services.AddAccessWayRabbitMQCore(builder.Configuration);
        builder.Services.AddAccessWayRabbitMQProducer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 仅装配 AccessWay RabbitMQ 通道消费者
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="instances">消费者最大数</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAccessWayRabbitMQOnlyConsumer<TAccessWayConsumer>(
        this WebApplicationBuilder builder,
        int instances = 1
    ) where TAccessWayConsumer : BaseAccessWayConsumer
    {
        if (builder.HasRegistered(IdentifierAddAccessWayRabbitMQOnlyConsumer))
        {
            return builder;
        }

        builder.Services.AddAccessWayRabbitMQCore(builder.Configuration);
        builder.Services.AddAccessWayRabbitMQConsumer<TAccessWayConsumer>(builder.Configuration, instances);

        return builder;
    }
}