using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Collector;

/// <summary>
/// 错误日志收集器
/// </summary>
public interface IErrorLogCollector
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="exception">异常</param>
    /// <returns>发送结果</returns>
    public bool Publish(Exception exception);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="operatorId"></param>
    /// <param name="requestInfo"></param>
    /// <returns>发送结果</returns>
    public bool Publish(
        Exception exception,
        long operatorId,
        IRequestInfo requestInfo = null
    );
}