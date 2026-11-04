using EasySoft.Core.Messaging.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;

/// <summary>
/// 全局信息收集器实现。只依赖 <see cref="IOutboundMessageBus"/> 契约，
/// 不感知 SlimMessageBus、RabbitMQ 或隔离单元的存在。
/// 注册为根容器单例，应用内所有程序集共享同一实例。
/// </summary>
public sealed class RabbitMQRabbitMQInformationCollector : IRabbitMQInformationCollector
{
    private readonly IOutboundMessageBus _bus;
 
    /// <param name="bus">
    /// 基础设施单元的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    public RabbitMQRabbitMQInformationCollector(
        [FromKeyedServices("infra-messaging")] IOutboundMessageBus bus
    )
    {
        _bus = bus;
    }

    /// <inheritdoc />
    public Task CollectAsync(RabbitMQInformationCollectedEvent @event, CancellationToken ct = default)
    {
        return _bus.Publish(@event, ct);
    }
}