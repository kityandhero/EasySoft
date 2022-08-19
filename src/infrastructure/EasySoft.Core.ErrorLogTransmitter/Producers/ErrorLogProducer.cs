using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.ErrorLogTransmitter.MessageQuery;
using EasySoft.Core.ExchangeRegulation.Query;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

public class ErrorLogProducer : IErrorLogProducer
{
    private readonly IQuery<IErrorLogExchange> _query;

    public ErrorLogProducer()
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();
    }

    private IQuery<IErrorLogExchange> GetQuery()
    {
        return _query;
    }

    public void Send(IErrorLogExchange log)
    {
        GetQuery().Send(log);
    }
}