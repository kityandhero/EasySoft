using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.GeneralLogTransceiver.Producers;

/// <inheritdoc />
public class GeneralLogProducer : IGeneralLogProducer
{
    private readonly IOutboundMessageBus _bus;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 一般日志发布器
    /// </summary>
    /// <param name="bus">
    /// 一般日志的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    /// <param name="applicationChannel"></param>
    public GeneralLogProducer(
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
        string message,
        CancellationToken cancellationToken = default
    )
    {
        return SendAsync(
            message,
            CustomValueType.PlainValue,
            "",
            CustomValueType.PlainValue,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public Task SendAsync(
        object message,
        CustomValueType messageValueType,
        CancellationToken cancellationToken = default
    )
    {
        return SendAsync(
            message,
            messageValueType,
            "",
            CustomValueType.PlainValue,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public Task SendAsync(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType,
        CancellationToken cancellationToken = default
    )
    {
        var entity = new GeneralLogMessage
        {
            MessageType = messageValueType.ToInt(),
            ContentType = contentValueType.ToInt(),
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        switch (messageValueType)
        {
            case CustomValueType.PlainValue:
                entity.Message = message.ToString() ?? "";
                break;

            default:
                entity.Message = JsonConvert.SerializeObject(message);
                break;
        }

        switch (contentValueType)
        {
            case CustomValueType.PlainValue:
                entity.Content = content.ToString() ?? "";
                break;

            default:
                entity.Content = JsonConvert.SerializeObject(content);
                break;
        }

        return SendAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public Task SendAsync(
        IGeneralLogMessage generalLogExchange,
        CancellationToken cancellationToken = default
    )
    {
        // await _capPublisher.PublishAsync(TransceiverConstant.GeneralLogMessage, generalLogExchange);

        // await _capPublisher.PublishAsync(TransceiverConstant.GeneralLogMessage, entity);

        return _bus.Publish(generalLogExchange, cancellationToken);
    }
}