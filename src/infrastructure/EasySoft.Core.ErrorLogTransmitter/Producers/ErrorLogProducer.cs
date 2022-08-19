using EasySoft.Core.Config.Channels;
using EasySoft.Core.ErrorLogTransmitter.Entities;
using EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.ErrorLogTransmitter.MessageQuery;
using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.UtilityTools.Entity;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

public class ErrorLogProducer : IErrorLogProducer
{
    private readonly IQuery<IErrorLogExchange> _query;

    private readonly IApplicationChannel _applicationChannel;

    public ErrorLogProducer(IApplicationChannel applicationChannel)
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();

        _applicationChannel = applicationChannel;
    }

    private IQuery<IErrorLogExchange> GetQuery()
    {
        return _query;
    }

    public void Send(IErrorLogExchange log)
    {
        GetQuery().Send(log);
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

        GetQuery().Send(entity);

        return entity;
    }
}