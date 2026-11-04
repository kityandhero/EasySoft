using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Consumers;

/// <summary>
/// 示例信息消费者。业务逻辑留空，仅示意消费者需进 DI。
/// 消费侧可靠性靠业务回查主库，本组件不做台账、不做重试编排。
/// </summary>
internal sealed class RabbitMQInformationConsumer : IConsumer<RabbitMQInformationCollectedEvent>
{
    public Task OnHandle(RabbitMQInformationCollectedEvent message, CancellationToken ct)
    {
        Console.WriteLine($"[consumer] received: {message.Category} / {message.Payload}");

        return Task.CompletedTask;
    }
}