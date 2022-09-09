using DotNetCore.CAP;
using EasySoft.Core.ExchangeRegulation.Enums;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Entities;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.UtilityTools.Core.Channels;
using Newtonsoft.Json;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

public class GeneralLogProducer : IGeneralLogProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    public GeneralLogProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    public IGeneralLogExchange Send(string message)
    {
        return Send(
            message,
            CustomValueType.PlainValue,
            "",
            CustomValueType.PlainValue
        );
    }

    public IGeneralLogExchange Send(object message, CustomValueType messageValueType)
    {
        return Send(
            message,
            messageValueType,
            "",
            CustomValueType.PlainValue
        );
    }

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

        _capPublisher.Publish(Configures.GetQueryName(), entity);

        return entity;
    }
}