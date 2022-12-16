using EasySoft.Core.ErrorLogTransmitter.Interfaces;

namespace EasySoft.Core.ErrorLogTransmitter.Producers;

/// <summary>
/// 错误日志生产者
/// </summary>
public interface IErrorLogProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="errorLogExchange"></param>
    void Send(IErrorLogExchange errorLogExchange);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IErrorLogExchange Send(Exception ex);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <returns></returns>
    IErrorLogExchange Send(Exception ex, long operatorId);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    IErrorLogExchange Send(Exception ex, long operatorId, RequestInfo requestInfo);
}