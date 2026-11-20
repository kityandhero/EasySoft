using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.Infrastructure.Collector;
using EasySoft.Core.TransceiverCore.Collectors;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.SqlLogTransceiver.Collectors;

/// <summary>
/// SQL日志收集器
/// </summary>
public class SqlLogCollector : BaseCollector<SqlLogTransitiveMessage>, ISqlLogCollector
{
    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 自研高性能文件写入实例
    /// </summary>
    private readonly DateRollingUtf8FileRecorder _fileRecorder;

    /// <summary>
    /// 自研批量MQ转发实例
    /// </summary>
    private readonly ISqlLogProducer _producer;

    /// <summary>
    /// 一般日志收集器
    /// </summary>
    /// <param name="loggerFactory">日志记录器工厂</param>
    /// <param name="applicationChannel">应用通道</param>
    /// <param name="producer">消息队列转发器</param>
    /// <param name="options">记录器配置</param>
    /// <param name="enableFileSink">是否启用文件落盘</param>
    /// <param name="enableTransferSink">是否启用MQ转发</param>
    /// <param name="mainBufferCapacity">主通道全局配额上限</param>
    /// <param name="sinkCapacityMultiplier">二级缓冲弹性倍率，推荐 2.0 ~ 2.5</param>
    /// <param name="metrics">指标采集，传null自动使用NullFileRecorderMetrics</param>
    public SqlLogCollector(
        ILoggerFactory loggerFactory,
        IApplicationChannel applicationChannel,
        ISqlLogProducer producer,
        IOptions<DateRollingUtf8FileRecorderOptions> options,
        bool enableFileSink,
        bool enableTransferSink,
        int mainBufferCapacity = 2000,
        double sinkCapacityMultiplier = 2.5,
        IFileRecorderMetrics metrics = null
    ) : base(
        loggerFactory,
        mainBufferCapacity,
        sinkCapacityMultiplier
    )
    {
        _applicationChannel = applicationChannel;

        _fileRecorder = new DateRollingUtf8FileRecorder(
            Logger,
            options,
            metrics
        );

        _producer = producer;

        if (!enableFileSink)
        {
            DisableFile();
        }
        else
        {
            EnableFile();
        }

        if (!enableTransferSink)
        {
            DisableTransfer();
        }
        else
        {
            EnableTransfer();
        }
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="commandString"></param>
    /// <param name="executeType"></param>
    /// <param name="stackTraceSnippet"></param>
    /// <param name="startMilliseconds"></param>
    /// <param name="durationMilliseconds"></param>
    /// <param name="firstFetchDurationMilliseconds"></param>
    /// <param name="errored"></param>
    /// <param name="collectMode"></param>
    /// <param name="databaseChannel"></param>
    /// <returns>发送结果</returns>
    public bool Publish(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int collectMode,
        string databaseChannel
    )
    {
        var entity = new SqlLogTransitiveMessage
        {
            CommandString = commandString,
            ExecuteType = executeType,
            StackTraceSnippet = stackTraceSnippet,
            StartMilliseconds = startMilliseconds,
            DurationMilliseconds = durationMilliseconds,
            FirstFetchDurationMilliseconds = firstFetchDurationMilliseconds,
            Errored = errored,
            CollectMode = collectMode,
            DatabaseChannel = databaseChannel,
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        return Publish(entity);
    }

    /// <summary>
    /// 对接高性能文件写入
    /// </summary>
    protected override Task WriteFileAsync(
        string json,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _fileRecorder.Write(json);

            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            Logger.LogAdvanceException(exception);

            return Task.FromException(exception);
        }
    }

    /// <summary>
    /// 对接MQ批量转发
    /// </summary>
    protected override async Task TransferAsync(
        string json,
        CancellationToken cancellationToken
    )
    {
        if (_producer == null)
        {
            Logger.LogAdvanceWarning("TransferAsync fail: _producer is null");

            return;
        }

        try
        {
            var transitiveMessage = Deserialize(json);

            if (transitiveMessage != null)
            {
                await _producer.SendAsync(transitiveMessage, cancellationToken);
            }
        }
        catch (Exception e)
        {
            Logger.LogAdvanceWarning($"TransferAsync fail: {e.Message}");
        }
    }
}