using EasySoft.Core.Messaging.Abstractions;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.ErrorLogTransceiver.Producers;

/// <inheritdoc />
public class ErrorLogProducer : IErrorLogProducer
{
    private readonly IOutboundMessageBus _bus;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 错误日志发布器
    /// </summary>
    /// <param name="bus">
    /// 错误日志的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    /// <param name="applicationChannel">应用通道</param>
    public ErrorLogProducer(
        [FromKeyedServices(TransceiverConstant.ErrorLogMessageServiceKey)]
        IOutboundMessageBus bus,
        IApplicationChannel applicationChannel
    )
    {
        _bus = bus;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public Task SendAsync(
        Exception ex,
        CancellationToken cancellationToken = default
    )
    {
        return SendAsync(
            ex,
            0,
            null,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public Task SendAsync(
        Exception ex,
        long operatorId,
        IRequestInfo requestInfo = null,
        CancellationToken cancellationToken = default
    )
    {
        var entity = new ErrorLogTransitiveMessage
        {
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        entity.Fill(
            ex,
            operatorId,
            requestInfo
        );

        return SendAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public Task SendAsync(
        IErrorLogTransitiveMessage errorLogTransitiveMessage,
        CancellationToken cancellationToken = default
    )
    {
        return _bus.Publish(errorLogTransitiveMessage, cancellationToken);
    }
}