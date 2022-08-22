using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.UtilityTools.Standard.Entity;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

public interface IErrorLogProducer
{
    public IErrorLogExchange Send(Exception ex);

    public IErrorLogExchange Send(Exception ex, long operatorId);

    public IErrorLogExchange Send(Exception ex, long operatorId, RequestInfo requestInfo);
}