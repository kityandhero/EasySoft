using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Producers;

/// <summary>
/// 错误日志生产者
/// </summary>
public interface IErrorLogProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        Exception exception,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        Exception exception,
        long operatorId,
        IRequestInfo requestInfo = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="errorLogTransitiveMessage"></param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        IErrorLogTransitiveMessage errorLogTransitiveMessage,
        CancellationToken cancellationToken = default
    );
}