using EasySoft.Core.ExchangeRegulation.Enums;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

public interface IGeneralLogProducer
{
    public IGeneralLogExchange Send(string message);

    public IGeneralLogExchange Send(object message, CustomValueType messageValueType);

    public IGeneralLogExchange Send(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType
    );
}