using EasySoft.Core.ErrorLogTransmitter.Entities;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;

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
    public async Task SendAsync(IErrorLogExchange errorLogExchange)
    {
        await _capPublisher.PublishAsync(TransmitterTopic.ErrorLogExchange, errorLogExchange);
    }

    /// <inheritdoc />
    public async Task<IErrorLogExchange> SendAsync(Exception ex)
    {
        return await SendAsync(ex, 0);
    }

    /// <inheritdoc />
    public async Task<IErrorLogExchange> SendAsync(
        Exception ex,
        long operatorId,
        IRequestInfo? requestInfo = null
    )
    {
        var entity = new ErrorLogExchange
        {
            Channel = _applicationChannel.GetChannel()
        };

        entity.Fill(ex, operatorId, requestInfo);

        await _capPublisher.PublishAsync(TransmitterTopic.ErrorLogExchange, entity);

        return entity;
    }
}