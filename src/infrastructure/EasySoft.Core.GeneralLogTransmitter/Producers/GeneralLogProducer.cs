namespace EasySoft.Core.GeneralLogTransmitter.Producers;

/// <inheritdoc />
public class GeneralLogProducer : IGeneralLogProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// 一般日志发送者
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public GeneralLogProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task<IGeneralLogMessage> SendAsync(string message)
    {
        return await SendAsync(
            message,
            CustomValueType.PlainValue,
            "",
            CustomValueType.PlainValue
        );
    }

    /// <inheritdoc />
    public async Task<IGeneralLogMessage> SendAsync(IGeneralLogMessage generalLogExchange)
    {
        await _capPublisher.PublishAsync(TransmitterTopic.GeneralLogMessage, generalLogExchange);

        return generalLogExchange;
    }

    /// <inheritdoc />
    public async Task<IGeneralLogMessage> SendAsync(object message, CustomValueType messageValueType)
    {
        return await SendAsync(
            message,
            messageValueType,
            "",
            CustomValueType.PlainValue
        );
    }

    /// <inheritdoc />
    public async Task<IGeneralLogMessage> SendAsync(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType
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

        await _capPublisher.PublishAsync(TransmitterTopic.GeneralLogMessage, entity);

        return entity;
    }
}