using EasySoft.UtilityTools.Standard.Messages;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

/// <inheritdoc />
public class ErrorLogProducer : IErrorLogProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public ErrorLogProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task SendAsync(IErrorLogMessage errorLogExchange)
    {
        await _capPublisher.PublishAsync(TransmitterTopic.ErrorLogMessage, errorLogExchange);
    }

    /// <inheritdoc />
    public async Task<IErrorLogMessage> SendAsync(Exception ex)
    {
        return await SendAsync(ex, 0);
    }

    /// <inheritdoc />
    public async Task<IErrorLogMessage> SendAsync(
        Exception ex,
        long operatorId,
        IRequestInfo? requestInfo = null
    )
    {
        var entity = new ErrorLogMessage
        {
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        entity.Fill(
            ex,
            operatorId,
            requestInfo
        );

        await _capPublisher.PublishAsync(TransmitterTopic.ErrorLogMessage, entity);

        return entity;
    }
}