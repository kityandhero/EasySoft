using EasySoft.Core.ChannelCheckTransmitter.Entities.implements;
using EasySoft.Core.ChannelCheckTransmitter.Entities.Interfaces;

namespace EasySoft.Core.ChannelCheckTransmitter.Producers;

/// <inheritdoc />
public class ChannelCheckProducer : IChannelCheckProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// AccessWayProducer
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public ChannelCheckProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task<IChannelCheckExchange> SendAsync()
    {
        var entity = new ChannelCheckExchange
        {
            Channel = _applicationChannel.GetChannel()
        };

        await _capPublisher.PublishAsync(TransmitterTopic.ChannelCheckExchange, entity);

        return entity;
    }
}