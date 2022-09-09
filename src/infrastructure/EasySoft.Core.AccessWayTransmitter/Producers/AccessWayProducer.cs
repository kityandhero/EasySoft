using DotNetCore.CAP;
using EasySoft.Core.AccessWayTransmitter.Entities;
using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.UtilityTools.Core.Channels;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

public class AccessWayProducer : IAccessWayProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    public AccessWayProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
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

        _capPublisher.Publish(Configures.GetQueryName(), entity);

        return entity;
    }
}