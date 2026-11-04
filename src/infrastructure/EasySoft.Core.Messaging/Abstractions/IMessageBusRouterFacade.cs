namespace EasySoft.Core.Messaging.Abstractions;

/// <summary>
/// 业务唯一入口。
/// </summary>
/// <remarks>
/// 业务注入此门面，不感知目标单元、vhost、连接；路由由配置 + 启动校验保障。
/// 运行期不做回退，路由缺失直接抛。
/// </remarks>
public interface IMessageBusRouterFacade
{
    /// <summary>
    /// 按消息类型路由到目标单元并发布。
    /// </summary>
    /// <param name="message">消息体。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <typeparam name="TMessage">消息类型。</typeparam>
    Task Publish<TMessage>(
        TMessage message,
        CancellationToken cancellationToken = default
    ) where TMessage : notnull;

    /// <summary>
    /// 按请求类型路由到目标单元并发送。
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
    /// 按请求类型路由到目标单元并发送
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