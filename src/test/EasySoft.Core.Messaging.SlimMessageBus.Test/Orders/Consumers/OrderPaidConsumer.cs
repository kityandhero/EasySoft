using EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Consumers;

/// <summary>
/// 订单支付消费者。业务逻辑留空，仅示意消费者需进 DI。
/// 消费侧可靠性靠业务回查主库，本组件不做台账、不做重试编排。
/// </summary>
internal sealed class OrderPaidConsumer : IConsumer<OrderPaid>
{
    public Task OnHandle(OrderPaid message, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}