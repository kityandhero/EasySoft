using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.Messaging.SlimMessageBus.Adapters;

/// <summary>
/// 出站消息总线适配器。直接包装 SlimMessageBus 公开的 <see cref="ICompositeMessageBus"/>。
/// </summary>
internal sealed class MessageBusOutboundAdapter : IOutboundMessageBus
{
    /// <summary>
    /// 复合总线实例。公开类型 <c>ICompositeMessageBus</c> 同时实现 <c>IMessageBus</c>。
    /// </summary>
    private readonly IMessageBus _bus;

    /// <summary>
    /// 注入 SlimMessageBus 总线。
    /// </summary>
    /// <param name="bus">当前隔离单元的 SlimMessageBus 总线。</param>
    public MessageBusOutboundAdapter(IMessageBus bus)
    {
        _bus = bus;
    }

    /// <inheritdoc />
    public Task Publish<TMessage>(
        TMessage message,
        CancellationToken cancellationToken = default
    ) where TMessage : notnull
    {
        return _bus.Publish(message, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : notnull
    {
        return Send<TRequest, TResponse>(
            request,
            null,
            null,
            null,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        string path = null,
        IDictionary<string, object> headers = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default
    ) where TRequest : notnull
    {
        return _bus.Send<TResponse, TRequest>(
            request,
            path,
            headers,
            timeout,
            cancellationToken
        );
    }
}