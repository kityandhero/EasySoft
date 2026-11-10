namespace EasySoft.Core.GeneralLogTransceiver.Producers;

/// <summary>
/// 一般日志发布器
/// </summary>
public interface IGeneralLogProducer
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        string message,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="messageValueType">消息类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        object message,
        CustomValueType messageValueType,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="messageValueType">消息类型</param>
    /// <param name="content">内容</param>
    /// <param name="contentValueType">内容类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="generalLogMessage">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        IGeneralLogMessage generalLogMessage,
        CancellationToken cancellationToken = default
    );
}