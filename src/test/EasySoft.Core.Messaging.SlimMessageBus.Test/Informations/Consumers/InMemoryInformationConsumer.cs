using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Consumers;

internal sealed class InMemoryInformationConsumer : IInMemoryInformationConsumer
{
    public async Task HandleAsync(InMemoryInformationCollectedEvent message, CancellationToken ct)
    {
        Console.WriteLine($"[consumer] received: {message.Category} / {message.Payload}");

        await Task.Delay(10, ct); // 每条处理 10ms

        // 消费逻辑。进程内、尽力而为，失败不重试。
        // return Task.CompletedTask;
    }
}