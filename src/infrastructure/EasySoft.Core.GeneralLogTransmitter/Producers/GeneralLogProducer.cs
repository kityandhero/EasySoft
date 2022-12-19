using EasySoft.Core.GeneralLogTransmitter.Entities;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;

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
    public IGeneralLogExchange Send(string message)
    {
        return Send(
            message,
            CustomValueType.PlainValue,
            "",
            CustomValueType.PlainValue
        );
    }

    /// <inheritdoc />
    public IGeneralLogExchange Send(object message, CustomValueType messageValueType)
    {
        return Send(
            message,
            messageValueType,
            "",
            CustomValueType.PlainValue
        );
    }

    /// <inheritdoc />
    public IGeneralLogExchange Send(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType
    )
    {
        var entity = new GeneralLogExchange
        {
            MessageType = messageValueType.ToInt(),
            ContentType = contentValueType.ToInt(),
            Channel = _applicationChannel.GetChannel()
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

        _capPublisher.Publish(TransmitterTopic.GeneralLogExchange, entity);

        return entity;
    }
}