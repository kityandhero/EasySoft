using EasySoft.Core.AccessWayTransmitter.Entities;
using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.Infrastructure.Transmitters;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.AccessWayTransmitter.Producers;

/// <inheritdoc />
public class AccessWayProducer : IAccessWayProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// AccessWayProducer
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public AccessWayProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task<IAccessWayExchange> SendAsync(
        IAccessWay accessWay
    )
    {
        var entity = new AccessWayExchange
        {
            Name = accessWay.Name,
            GuidTag = accessWay.GuidTag,
            RelativePath = accessWay.RelativePath,
            Expand = accessWay.Expand,
            Group = accessWay.Group,
            Channel = _applicationChannel.GetChannel()
        };

        await _capPublisher.PublishAsync(TransmitterTopic.AccessWayExchange, entity);

        return entity;
    }
}