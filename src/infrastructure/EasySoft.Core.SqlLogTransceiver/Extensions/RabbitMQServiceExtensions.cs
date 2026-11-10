using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.Core.SqlLogTransceiver.Consumers;
using EasySoft.Core.SqlLogTransceiver.Producers;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.SqlLogTransceiver.Extensions;

/// <summary>
/// RabbitMQServiceExtensions
/// </summary>
public static class RabbitMQServiceExtensions
{
    /// <summary>
    /// 声明 "SqlLog" 子总线的基础配置：连接串。
    /// 生产方服务和消费方服务都必须调用一次，作为子总线的存在前提。
    /// 不声明任何生产者或消费者。
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集合</param>
    public static IServiceCollection AddSqlLogRabbitMQCore(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSlimMessageBus(messageBusBuilder =>
            {
                messageBusBuilder.AddChildBus(
                    TransceiverConstant.SqlLogMessageTransmitter,
                    child =>
                    {
                        child.WithProviderRabbitMQ(cfg =>
                            {
                                var configurationPath = TransceiverConstant
                                    .GetSqlLogMessageRabbitMQConnectionString();

                                cfg.ConnectionString = configuration[configurationPath]
                                                       ?? throw new InvalidOperationException(
                                                           $"缺少 SqlLogMessage RabbitMQ 连接串[{configurationPath}]。"
                                                       );

                                // 交换器持久化：影响本子总线内所有生产者的交换器声明。
                                cfg.UseExchangeDefaults(true);

                                // 队列持久化：影响本子总线内所有消费者的队列声明。
                                cfg.UseQueueDefaults(true);

                                // 驱动层消费并发上限。与消费端 .Instances(n) 共同作用。
                                // 默认 1；大于 1 会打破单个消费者的消息处理顺序。
                                // 两者都大于 1 时，实际并发受更严格的一方约束
                                cfg.ConnectionFactory.ConsumerDispatchConcurrency = 3;
                            }
                        );
                    }
                );
            }
        );

        // 出站口暴露给上层基础设施（生产方需要）
        services.AddKeyedOutboundBus(TransceiverConstant.SqlLogMessageServiceKey);

        return services;
    }

    /// <summary>
    /// 声明 SqlLog 的生产者与出站口。
    /// 只在负责发消息的服务中调用，不引用消费者实现。
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集合</param>
    public static IServiceCollection AddSqlLogRabbitMQProducer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSlimMessageBus(messageBusBuilder =>
            {
                messageBusBuilder.AddChildBus(
                    TransceiverConstant.SqlLogMessageTransmitter,
                    child =>
                    {
                        child.Produce<ISqlLogMessage>(x => x
                            .Exchange(GetExchange(configuration), ExchangeType.Direct)
                            .RoutingKeyProvider((_, _) => GetRoutingKey(configuration))
                        );
                    }
                );
            }
        );

        services.AddSingleton<ISqlLogProducer, SqlLogProducer>();

        return services;
    }

    /// <summary>
    /// 声明 SqlLog 的消费者与队列绑定。
    /// 只在负责收消息的服务中调用，引用消费者实现程序集，不引用 RabbitMQRabbitMQInformationCollector。
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集合</param>
    /// <param name="instances">消费者最大数</param>
    public static IServiceCollection AddSqlLogRabbitMQConsumer<TSqlLogConsumer>(
        this IServiceCollection services,
        IConfiguration configuration,
        int instances = 1
    ) where TSqlLogConsumer : BaseSqlLogConsumer
    {
        services.AddSlimMessageBus(mbb =>
            {
                mbb.AddChildBus(
                    TransceiverConstant.SqlLogMessageTransmitter,
                    child =>
                    {
                        child.Consume<ISqlLogMessage>(x =>
                            {
                                x.Queue(GetQueue(configuration))
                                    .ExchangeBinding(GetExchange(configuration), GetRoutingKey(configuration))
                                    .WithConsumer<TSqlLogConsumer>()
                                    .Instances(instances); // 消费端限流：单消费者并发数
                            }
                        );
                    }
                );
            }
        );

        services.AddTransient<TSqlLogConsumer>();

        return services;
    }

    private static string GetExchange(IConfiguration configuration)
    {
        var prefix = AppSettingAssist.GetGlobalPrefix(configuration);

        return StringAssist.Join(
            [prefix, TransceiverConstant.SqlLogMessageExchange],
            "."
        );
    }

    private static string GetRoutingKey(IConfiguration configuration)
    {
        var prefix = AppSettingAssist.GetGlobalPrefix(configuration);

        return StringAssist.Join(
            [prefix, TransceiverConstant.SqlLogMessageRoutingKey],
            "."
        );
    }

    private static string GetQueue(IConfiguration configuration)
    {
        var prefix = AppSettingAssist.GetGlobalPrefix(configuration);

        return StringAssist.Join(
            [prefix, TransceiverConstant.SqlLogMessageQueue],
            "."
        );
    }
}