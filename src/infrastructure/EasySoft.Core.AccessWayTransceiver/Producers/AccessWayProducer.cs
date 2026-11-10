using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.AccessWayTransceiver.Producers;

/// <inheritdoc />
public class AccessWayProducer : IAccessWayProducer
{
    private readonly IOutboundMessageBus _bus;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// AccessWay 发布器
    /// </summary>
    /// <param name="bus">
    /// AccessWay 的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    /// <param name="applicationChannel">应用通道</param>
    public AccessWayProducer(
        [FromKeyedServices(TransceiverConstant.AccessWayMessageServiceKey)]
        IOutboundMessageBus bus,
        IApplicationChannel applicationChannel
    )
    {
        _bus = bus;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public Task SendAsync(
        IAccessWay accessWay,
        CancellationToken cancellationToken = default
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

        return _bus.Publish(entity, cancellationToken);
    }
}