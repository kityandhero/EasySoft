using EasySoft.Core.ErrorLogTransmitter.Interfaces;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

public interface IErrorLogProducer
{
    public void Send(IErrorLogExchange log);
}