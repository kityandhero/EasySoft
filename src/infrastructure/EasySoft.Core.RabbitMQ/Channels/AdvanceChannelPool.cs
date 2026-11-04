using EasySoft.Core.RabbitMQ.Connections;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.Core.RabbitMQ.Channels;

/// <summary>
/// 封装的 RabbitMQ IChannel 通道池，线程安全，支持发布确认
/// </summary>
public class AdvanceChannelPool
{
    private readonly IConnection _connection;
    private readonly ILogger<AdvanceChannelPool> _logger;
    private readonly ConcurrentQueue<IChannel> _idleChannels = new();
    private readonly SemaphoreSlim _semaphore;
    private readonly CreateChannelOptions _channelOptions;
    private int _createdChannelCount;

    /// <summary>
    /// 通道池构造
    /// </summary>
    /// <param name="advanceConnection">连接包装</param>
    /// <param name="logger">日志</param>
    /// <param name="maxChannelCount">池最大通道数量，建议和CPU核心数一致，例如8</param>
    /// <param name="publisherConfirmationsEnabled">发布方确认是否启用</param>
    /// <param name="publisherConfirmationTrackingEnabled">发布方确认跟踪是否启用</param> 
    public AdvanceChannelPool(
        IAdvanceConnection advanceConnection,
        ILogger<AdvanceChannelPool> logger,
        int maxChannelCount = 8,
        bool publisherConfirmationsEnabled = true,
        bool publisherConfirmationTrackingEnabled = true
    )
    {
        _connection = advanceConnection.Connection;
        _logger = logger;
        _semaphore = new SemaphoreSlim(maxChannelCount, maxChannelCount);
        _channelOptions = new CreateChannelOptions(
            publisherConfirmationsEnabled,
            publisherConfirmationTrackingEnabled,
            consumerDispatchConcurrency: 1
        );
    }

    /// <summary>
    /// 获取可用通道（优先取空闲，失效通道自动丢弃）
    /// </summary>
    /// <returns>IChannel</returns>
    public async Task<IChannel> RentChannelAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            // 取出有效空闲通道
            while (_idleChannels.TryDequeue(out var ch))
            {
                if (ch.IsOpen)
                {
                    return ch;
                }

                // 通道已经关闭，直接释放
                await ch.DisposeAsync();

                Interlocked.Decrement(ref _createdChannelCount);
            }

            if (_connection == null)
            {
                _logger.LogError("RabbitMQ 连接为空");

                throw new UnhandledException("RabbitMQ 连接为空");
            }

            // 无空闲通道，新建通道
            _logger.LogInformation("通道池新建IChannel，当前总通道数:{Count}", _createdChannelCount + 1);

            var channel = await _connection.CreateChannelAsync(_channelOptions);

            Interlocked.Increment(ref _createdChannelCount);

            // 订阅发布确认异步事件
            channel.BasicAcksAsync += (_, ea) =>
            {
                _logger.LogDebug(
                    "消息Ack，DeliveryTag:{Tag}, Multiple:{Mul}",
                    ea.DeliveryTag,
                    ea.Multiple
                );

                return Task.CompletedTask;
            };

            channel.BasicNacksAsync += (_, ea) =>
            {
                _logger.LogWarning(
                    "消息Nack，DeliveryTag:{Tag}, Multiple:{Mul}",
                    ea.DeliveryTag,
                    ea.Multiple
                );

                return Task.CompletedTask;
            };

            return channel;
        }
        catch
        {
            _semaphore.Release();

            throw;
        }
    }

    /// <summary>
    /// 归还通道：通道正常打开则放回空闲队列；已关闭直接销毁
    /// </summary>
    /// <param name="channel">租用的通道</param>
    public async Task ReturnChannelAsync(IChannel channel)
    {
        try
        {
            if (channel is { IsOpen: true })
            {
                _idleChannels.Enqueue(channel);
            }
            else
            {
                if (channel != null)
                {
                    await channel.DisposeAsync();
                }

                Interlocked.Decrement(ref _createdChannelCount);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// 清理池内所有通道，服务停止时调用
    /// </summary>
    public async Task CleanAllAsync()
    {
        while (_idleChannels.TryDequeue(out var ch))
        {
            await ch.CloseAsync();

            await ch.DisposeAsync();

            Interlocked.Decrement(ref _createdChannelCount);
        }
    }
}