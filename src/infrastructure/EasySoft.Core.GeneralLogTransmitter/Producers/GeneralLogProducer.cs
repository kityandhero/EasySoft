using EasySoft.Core.ExchangeRegulation.Enums;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.GeneralLogTransmitter.Entities;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.GeneralLogTransmitter.MessageQuery;
using EasySoft.UtilityTools.Core.Channels;
using Newtonsoft.Json;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

public class GeneralLogProducer : IGeneralLogProducer
{
    private readonly IQuery<IGeneralLogExchange> _query;

    private readonly IApplicationChannel _applicationChannel;

    public GeneralLogProducer(IApplicationChannel applicationChannel)
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();

        _applicationChannel = applicationChannel;
    }

    private IQuery<IGeneralLogExchange> GetQuery()
    {
        return _query;
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

        GetQuery().Send(entity);

        return entity;
    }
}