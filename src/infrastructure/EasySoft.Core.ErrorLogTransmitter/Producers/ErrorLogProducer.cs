using DotNetCore.CAP;
using EasySoft.Core.ErrorLogTransmitter.Entities;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Entity;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

public class ErrorLogProducer : IErrorLogProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    public ErrorLogProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    public void Send(IErrorLogExchange log)
    {
        _capPublisher.Publish(Configures.GetQueryName(), log);
    }

    public IErrorLogExchange Send(Exception ex)
    {
        return Send(ex, 0, null);
    }

    public IErrorLogExchange Send(Exception ex, long operatorId)
    {
        return Send(ex, operatorId, null);
    }

    public IErrorLogExchange Send(Exception ex, long operatorId, RequestInfo? requestInfo)
    {
        var entity = new ErrorLogExchange
        {
            Channel = _applicationChannel.GetChannel()
        };

        entity.Fill(ex, operatorId, requestInfo);

        _capPublisher.Publish(Configures.GetQueryName(), entity);

        return entity;
    }
}