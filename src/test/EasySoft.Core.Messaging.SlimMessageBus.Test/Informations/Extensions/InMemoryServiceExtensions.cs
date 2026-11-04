using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.BackgroundServices;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Extensions;

public static class InMemoryServiceExtensions
{
    /// <summary>
    /// 注册内存队列版本的 InformationCollector。
    /// <para>
    /// 与 RabbitMQ 版本的注册入口分开：
    /// RabbitMQ 版本拆成 Core / Producer / Consumer 三个入口，因为生产方和消费方可能部署在不同服务。
    /// 内存队列版本生产者和消费者在同一个进程内，天然紧耦合，合并为一个入口。
    /// </para>
    /// </summary>
    /// <param name="services">根容器注册集合。</param>
    /// <param name="configure">队列容量与满队列策略配置。不传则使用默认值（Capacity=1000，DropWrite）。</param>
    public static IServiceCollection AddInformationInMemory(
        this IServiceCollection services,
        Action<InMemoryInformationCollectorOptions> configure = null
    )
    {
        var options = new InMemoryInformationCollectorOptions();
        configure?.Invoke(options);

        services.AddSingleton(options);
        services.AddSingleton<InMemoryInformationCollector>();
        services.AddSingleton<IInMemoryInformationCollector>(sp => sp.GetRequiredService<InMemoryInformationCollector>()
        );

        // 后台泵由 Generic Host 驱动。
        services.AddHostedService<InMemoryInformationSchedulingService>();

        // 默认消费者。业务可覆盖为自己的实现。
        services.TryAddScoped<IInMemoryInformationConsumer, InMemoryInformationConsumer>();

        return services;
    }
}