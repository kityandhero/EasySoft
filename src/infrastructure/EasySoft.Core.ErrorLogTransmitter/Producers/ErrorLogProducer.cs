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
    public void Send(IErrorLogExchange errorLogExchange)
    {
        _capPublisher.Publish(TransmitterTopic.ErrorLogExchange, errorLogExchange);
    }

    /// <inheritdoc />
    public IErrorLogExchange Send(Exception ex)
    {
        return Send(ex, 0, null);
    }

    /// <inheritdoc />
    public IErrorLogExchange Send(Exception ex, long operatorId)
    {
        return Send(ex, operatorId, null);
    }

    /// <inheritdoc />
    public IErrorLogExchange Send(Exception ex, long operatorId, RequestInfo? requestInfo)
    {
        var entity = new ErrorLogExchange
        {
            Channel = _applicationChannel.GetChannel()
        };

        entity.Fill(ex, operatorId, requestInfo);

        _capPublisher.Publish(TransmitterTopic.ErrorLogExchange, entity);

        return entity;
    }
}