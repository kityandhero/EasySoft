namespace EasySoft.Core.Messaging.Abstractions;

/// <summary>
/// 传输无关的出站契约。
/// </summary>
/// <remarks>
/// 每个隔离单元内至多注册一个实现，基础库只依赖此接口。
/// </remarks>
public interface IOutboundMessageBus
{
    /// <summary>
    /// 发布一条消息（点对多）。
    /// </summary>
    /// <param name="message">消息体。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <typeparam name="TMessage">消息类型，决定序列化格式与路由。</typeparam>
    Task Publish<TMessage>(
        TMessage message,
        CancellationToken cancellationToken = default
    ) where TMessage : notnull;

    /// <summary>
    /// 发送请求并返回响应。
    /// </summary>
    /// <param name="request">请求体。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <typeparam name="TRequest">请求类型。</typeparam>
    /// <typeparam name="TResponse">响应类型。</typeparam>
    /// <returns>响应体。</returns>
    Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : notnull;

    /// <summary>
    /// 发送请求并返回响应。
    /// </summary>
    /// <param name="request">请求体。</param>
    /// <param name="path">目标路径；<c>null</c> 由传输层按消息类型解析默认值。</param>
    /// <param name="headers">消息头；<c>null</c> 表示无额外头。</param>
    /// <param name="timeout">单次超时；<c>null</c> 用总线级默认超时。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <typeparam name="TRequest">请求类型。</typeparam>
    /// <typeparam name="TResponse">响应类型。</typeparam>
    /// <returns>响应体。</returns>
    Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        string path,
        IDictionary<string, object> headers,
        TimeSpan? timeout,
        CancellationToken cancellationToken = default
    ) where TRequest : notnull;
}