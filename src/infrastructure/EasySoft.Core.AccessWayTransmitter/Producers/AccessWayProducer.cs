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
        IAccessWayPersistence accessWayPersistence
    )
    {
        var entity = new AccessWayExchange
        {
            Name = accessWayPersistence.Name,
            GuidTag = accessWayPersistence.GuidTag,
            RelativePath = accessWayPersistence.RelativePath,
            Expand = accessWayPersistence.Expand,
            Group = accessWayPersistence.Group,
            Channel = _applicationChannel.GetChannel()
        };

        await _capPublisher.PublishAsync(TransmitterTopic.AccessWayExchange, entity);

        return entity;
    }
}