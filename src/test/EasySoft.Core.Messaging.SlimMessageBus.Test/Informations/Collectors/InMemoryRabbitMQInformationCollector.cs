using System.Threading.Channels;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Options;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;

internal sealed class InMemoryInformationCollector : IInMemoryInformationCollector
{
    private readonly Channel<InMemoryInformationCollectedEvent> _channel;

    /// <summary>成功入队的消息数。</summary>
    private long _enqueuedCount;

    /// <summary>因队列满而被丢弃的消息数。</summary>
    private long _droppedCount;

    public InMemoryInformationCollector(InMemoryInformationCollectorOptions options)
    {
        _channel = Channel.CreateBounded<InMemoryInformationCollectedEvent>(
            new BoundedChannelOptions(options.Capacity)
            {
                FullMode = options.FullMode,
                SingleReader = true,
                SingleWriter = false
            }
        );
    }

    /// <summary>供后台泵读取 Channel。internal，不暴露给业务代码。</summary>
    internal ChannelReader<InMemoryInformationCollectedEvent> Reader => _channel.Reader;

    /// <summary>累计成功入队数，用于测试和监控。</summary>
    public long EnqueuedCount => Interlocked.Read(ref _enqueuedCount);

    /// <summary>累计丢弃数，用于测试和监控。</summary>
    public long DroppedCount => Interlocked.Read(ref _droppedCount);

    public Task CollectAsync(InMemoryInformationCollectedEvent message, CancellationToken ct = default)
    {
        Interlocked.Increment(ref _enqueuedCount);

        //先进先出模式
        return _channel.Writer.WriteAsync(message, ct).AsTask();

        // 主动抛弃模式
        // 队列满时行为由 FullMode 决定：DropWrite 静默丢弃当前消息，DropOldest 丢弃最旧消息，Wait 阻塞。
        if (_channel.Writer.TryWrite(message))
        {
            Interlocked.Increment(ref _enqueuedCount);
        }
        else
        {
            Interlocked.Increment(ref _droppedCount);
        }

        return Task.CompletedTask;
    }
}