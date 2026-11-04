using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.BackgroundServices;

/// <summary>
/// 从有界 Channel 读出消息并执行消费者的后台服务。
/// 由 Generic Host 在启动时调用 <see cref="ExecuteAsync"/>，在停机时通过 stoppingToken 取消。
/// 停机时 Channel 中未处理的消息直接丢弃，符合内存队列的语义。
/// </summary>
internal class InMemoryInformationSchedulingService : BackgroundService
{
    private readonly InMemoryInformationCollector _collector;
    private readonly IServiceProvider _sp;

    public InMemoryInformationSchedulingService(
        InMemoryInformationCollector collector,
        IServiceProvider sp
    )
    {
        _collector = collector;
        _sp = sp;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var @event in _collector.Reader.ReadAllAsync(stoppingToken))
            {
                // 每条消息创建独立 DI 作用域，避免 Scoped 消费者被跨消息复用。
                using var scope = _sp.CreateScope();
                var consumer = scope.ServiceProvider
                    .GetRequiredService<IInMemoryInformationConsumer>();

                try
                {
                    await consumer.HandleAsync(@event, stoppingToken);
                }
                catch
                {
                    // 内存队列不承诺可靠投递：消费失败不重试，不写死信，直接丢弃。
                    // 需要可观测性的话，在这里记录日志或计数，不要向外抛。
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 宿主停机，正常退出。Channel 中剩余消息丢弃。
        }
    }
}