using EasySoft.Core.ChannelCheckTransceiver.Entities.implements;
using EasySoft.Core.ChannelCheckTransceiver.Entities.Interfaces;
using EasySoft.Core.Messaging.Abstractions;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.ChannelCheckTransceiver.Producers;

/// <inheritdoc />
public class ChannelCheckProducer : IChannelCheckProducer
{
    private readonly IOutboundMessageBus _bus;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// ChannelCheck 发布器
    /// </summary>
    /// <param name="bus">
    /// ChannelCheck 的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    /// <param name="applicationChannel">应用通道</param>
    public ChannelCheckProducer(
        [FromKeyedServices(TransceiverConstant.GeneralLogMessageServiceKey)]
        IOutboundMessageBus bus,
        IApplicationChannel applicationChannel
    )
    {
        _bus = bus;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public Task SendAsync(
        CancellationToken cancellationToken = default
    )
    {
        var entity = new ChannelCheckMessage
        {
            Channel = _applicationChannel.GetChannel().ToValue()
        };

        return _bus.Publish(entity, cancellationToken);
    }
}