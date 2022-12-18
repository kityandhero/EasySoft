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
    Task SendAsync(IErrorLogExchange errorLogExchange);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    Task<IErrorLogExchange> SendAsync(Exception ex);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    Task<IErrorLogExchange> SendAsync(Exception ex, long operatorId, RequestInfo? requestInfo = null);
}