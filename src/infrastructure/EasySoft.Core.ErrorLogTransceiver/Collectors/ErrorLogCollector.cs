using EasySoft.Core.AdvanceFileRecord;
using EasySoft.Core.TransceiverCore.Collectors;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.ErrorLogTransceiver.Collectors;

/// <summary>
/// 错误日志收集器
/// </summary>
public class ErrorLogCollector : BaseCollector<ErrorLogTransitiveMessage>
{
    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 自研高性能文件写入实例
    /// </summary>
    private readonly DateRollingUtf8FileRecorder _fileRecorder;

    /// <summary>
    /// 自研批量MQ转发实例
    /// </summary>
    private readonly IErrorLogProducer _producer;

    /// <summary>
    /// 错误日志收集器
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
    public ErrorLogCollector(
        ILoggerFactory loggerFactory,
        IApplicationChannel applicationChannel,
        IErrorLogProducer producer,
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
    /// <param name="exception">异常</param>
    /// <returns>发送结果</returns>
    public bool Publish(Exception exception)
    {
        return Publish(
            exception,
            0,
            null
        );
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <returns>发送结果</returns>
    public bool Publish(
        Exception exception,
        long operatorId,
        IRequestInfo requestInfo = null
    )
    {
        var entity = new ErrorLogTransitiveMessage
        {
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        entity.Fill(
            exception,
            operatorId,
            requestInfo
        );

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