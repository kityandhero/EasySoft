using EasySoft.Core.AccessWayTransmitter.Entities;
using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.AccessWayTransmitter.MessageQuery;
using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.UtilityTools.Core.Channels;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public class AccessWayProducer : IAccessWayProducer
{
    private readonly IQuery<IAccessWayExchange> _query;

    private readonly IApplicationChannel _applicationChannel;

    public AccessWayProducer(IApplicationChannel applicationChannel)
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();

        _applicationChannel = applicationChannel;
    }

    private IQuery<IAccessWayExchange> GetQuery()
    {
        return _query;
    }

    public IAccessWayExchange Send(
        string guidTag,
        string name,
        string path,
        string competence
    )
    {
        var entity = new AccessWayExchange
        {
            GuidTag = guidTag,
            Name = name,
            RelativePath = path,
            Expand = competence,
            Channel = _applicationChannel.GetChannel()
        };

        GetQuery().Send(entity);

        return entity;
    }
}