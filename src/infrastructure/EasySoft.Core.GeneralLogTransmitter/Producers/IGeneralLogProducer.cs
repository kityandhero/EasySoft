using EasySoft.Core.GeneralLogTransmitter.Interfaces;

namespace EasySoft.Core.GeneralLogTransmitter.Producers;

public interface IGeneralLogProducer
{
    public void Send(IGeneralLogExchange log);
}