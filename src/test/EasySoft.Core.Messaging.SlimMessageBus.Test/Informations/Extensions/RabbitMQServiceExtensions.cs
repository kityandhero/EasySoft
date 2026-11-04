using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Extensions;

public static class RabbitMQServiceExtensions
{
    /// <summary>
    /// 声明 "infra" 子总线的基础配置：连接串。
    /// 生产方服务和消费方服务都必须调用一次，作为子总线的存在前提。
    /// 不声明任何生产者或消费者。
    /// </summary>
    public static IServiceCollection AddInfraBusRabbitMQCore(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSlimMessageBus(mbb =>
            {
                mbb.AddChildBus(
                    "infra",
                    child =>
                    {
                        child.WithProviderRabbitMQ(cfg =>
                            {
                                cfg.ConnectionString = configuration["Messaging:Buses:infra:ConnectionString"]
                                                       ?? throw new InvalidOperationException("缺少 infra 总线连接串。");

                                // 交换器持久化：影响本子总线内所有生产者的交换器声明。
                                cfg.UseExchangeDefaults(true);

                                // 队列持久化：影响本子总线内所有消费者的队列声明。
                                cfg.UseQueueDefaults(true);

                                // 驱动层消费并发上限。与消费端 .Instances(n) 共同作用。
                                // 默认 1；大于 1 会打破单个消费者的消息处理顺序。
                                // 两者都大于 1 时，实际并发受更严格的一方约束
                                cfg.ConnectionFactory.ConsumerDispatchConcurrency = 1;
                            }
                        );
                    }
                );
            }
        );

        // 出站口暴露给上层基础设施（生产方需要）
        services.AddKeyedOutboundBus("infra-messaging");

        return services;
    }

    /// <summary>
    /// 声明 RabbitMQInformationCollectedEvent 的生产者与出站口。
    /// 只在负责发消息的服务中调用，引用 Infra.Contracts，不引用消费者实现。
    /// </summary>
    public static IServiceCollection AddInfraRabbitMQProducer(
        this IServiceCollection services
    )
    {
        services.AddSlimMessageBus(mbb =>
            {
                mbb.AddChildBus(
                    "infra",
                    child =>
                    {
                        child.Produce<RabbitMQInformationCollectedEvent>(x => x
                            .Exchange("info.collected", ExchangeType.Direct)
                            .RoutingKeyProvider((message, context) => "info.default")
                        );
                    }
                );
            }
        );

        services.AddSingleton<IRabbitMQInformationCollector, RabbitMQRabbitMQInformationCollector>();

        return services;
    }

    /// <summary>
    /// 声明 RabbitMQInformationCollectedEvent 的消费者与队列绑定。
    /// 只在负责收消息的服务中调用，引用消费者实现程序集，不引用 RabbitMQRabbitMQInformationCollector。
    /// </summary>
    public static IServiceCollection AddInfraRabbitMQConsumer(
        this IServiceCollection services,
        int instances = 1
    )
    {
        services.AddSlimMessageBus(mbb =>
            {
                mbb.AddChildBus(
                    "infra",
                    child =>
                    {
                        child.Consume<RabbitMQInformationCollectedEvent>(x =>
                            {
                                x.Queue("info.collected.queue")
                                    .ExchangeBinding("info.collected", "info.default")
                                    .WithConsumer<RabbitMQInformationConsumer>()
                                    .Instances(instances); // 消费端限流：单消费者并发数
                            }
                        );
                    }
                );
            }
        );

        services.AddTransient<RabbitMQInformationConsumer>();

        return services;
    }
}