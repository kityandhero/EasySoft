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
    Task SendAsync(IErrorLogMessage errorLogExchange);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    Task<IErrorLogMessage> SendAsync(Exception ex);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    Task<IErrorLogMessage> SendAsync(Exception ex, long operatorId, IRequestInfo? requestInfo = null);
}