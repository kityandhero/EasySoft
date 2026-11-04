using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.Messaging.SlimMessageBus.Facades;

/// <summary>
/// 路由门面实现。
/// </summary>
/// <remarks>
/// 注入根容器的 <c>ICompositeMessageBus</c>（实现 <c>IMessageBus</c>），
/// 由其按消息类型自动路由到声明了 <c>Produce&lt;T&gt;()</c> 的子总线。
/// 门面本身不查表、不回退、不感知子总线名。
/// </remarks>
internal sealed class MessageBusRouterFacade : IMessageBusRouterFacade
{
    /// <summary>
    /// 复合总线实例，由根容器注入。hybrid 模式下实现为 <c>ICompositeMessageBus</c>。
    /// </summary>
    private readonly IMessageBus _bus;

    /// <summary>
    /// 注入复合总线。
    /// </summary>
    /// <param name="bus">根容器的 <see cref="IMessageBus"/>，必须非空。</param>
    public MessageBusRouterFacade(IMessageBus bus)
    {
        _bus = bus;
    }

    /// <inheritdoc />
    public Task Publish<TMessage>(TMessage message, CancellationToken ct = default)
        where TMessage : notnull
    {
        return _bus.Publish(message, cancellationToken: ct);
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : notnull
    {
        return _bus.Send<TResponse, TRequest>(request, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TRequest, TResponse>(
        TRequest request,
        string path,
        IDictionary<string, object> headers,
        TimeSpan? timeout,
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