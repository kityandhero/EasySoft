using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.GeneralLogTransmitter.MessageQuery;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

public class GeneralLogProducer : IGeneralLogProducer
{
    private readonly IQuery<IGeneralLogExchange> _query;

    public GeneralLogProducer()
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();
    }

    private IQuery<IGeneralLogExchange> GetQuery()
    {
        return _query;
    }

    public void Send(IGeneralLogExchange log)
    {
        GetQuery().Send(log);
    }
}