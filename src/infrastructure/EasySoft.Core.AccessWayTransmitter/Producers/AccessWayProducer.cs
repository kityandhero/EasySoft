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
    public async Task<IAccessWayMessage> SendAsync(
        IAccessWay accessWay
    )
    {
        var entity = new AccessWayMessage
        {
            Name = accessWay.Name,
            GuidTag = accessWay.GuidTag,
            RelativePath = accessWay.RelativePath,
            Expand = accessWay.Expand,
            Group = accessWay.Group,
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        await _capPublisher.PublishAsync(TransmitterTopic.AccessWayMessage, entity);

        return entity;
    }
}