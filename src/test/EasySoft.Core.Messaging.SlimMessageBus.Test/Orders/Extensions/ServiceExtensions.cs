using EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// 注册订单业务消息组件。
    /// 声明独立的 "order" 子总线，包含生产者与消费者声明。不感知任何其他子总线。
    /// <para>
    /// 路由保障：<see cref="OrderCreated"/> 的 <c>Produce&lt;T&gt;()</c> 只在 "order" 子总线声明，
    /// 复合总线收到该类型的发布请求时唯一匹配到 "order" 子总线。
    /// </para>
    /// </summary>
    public static IServiceCollection AddOrderMessaging(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSlimMessageBus(mbb =>
            {
                mbb.AddChildBus(
                    "order",
                    child =>
                    {
                        child.WithProviderRabbitMQ(cfg =>
                            {
                                cfg.ConnectionString = configuration["Messaging:Buses:order:ConnectionString"]
                                                       ?? throw new InvalidOperationException("缺少 order 总线连接串。");
                                cfg.UseQueueDefaults(true);
                            }
                        );

                        child.Produce<OrderCreated>(x => x
                            .Exchange("orders", ExchangeType.Direct)
                        );

                        // QueueArguments 是 IDictionary，索引器赋值不能链式调用，必须单独语句。
                        child.Consume<OrderPaid>(x =>
                            {
                                x.Queue("order.paid")
                                    .ExchangeBinding("orders", "paid")
                                    .WithConsumer<OrderPaidConsumer>();

                                x.DeadLetterExchange("orders.dlx", ExchangeType.Direct);
                            }
                        );
                    }
                );
            }
        );

        // WithConsumer<T>() 只是声明，实例由 SMB 从容器解析，必须注册。
        services.AddTransient<OrderPaidConsumer>();

        return services;
    }
}