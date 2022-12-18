using EasySoft.Core.AccessWayTransmitter.Entities;
using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.Infrastructure.Transmitters;

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

    public async Task<IAccessWayExchange> SendAsync(
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

        await _capPublisher.PublishAsync(TransmitterTopic.AccessWayExchange, entity);

        return entity;
    }
}