using EasySoft.Core.Messaging.Abstractions;
using EasySoft.Core.Messaging.Observers;

namespace EasySoft.Core.Messaging.SlimMessageBus.Interceptors;

/// <summary>
/// Send 链路拦截器。
/// </summary>
/// <remarks>
/// SlimMessageBus 的 <see cref="ISendInterceptor{TRequest,TResponse}"/>。
/// 职责与 Publish 拦截器一致，但是 Send 需要把响应原样传回调用方，
/// <c>next</c> 与返回值携带 <typeparamref name="TResponse"/>。
/// </remarks>
/// <typeparam name="TRequest">
/// 请求类型。SlimMessageBus 运行时按请求类型解析闭合泛型实例，
/// 必须与调用 <c>Send&lt;TRequest, TResponse&gt;</c> 时传入的请求类型一致。
/// </typeparam>
/// <typeparam name="TResponse">
/// 响应类型。请求-响应模式下的返回值类型，必须与调用方期望的一致。
/// </typeparam>
internal sealed class TracingSendInterceptor<TRequest, TResponse> : ISendInterceptor<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// 观测实现。默认 <see cref="ActivityPipelineObserver"/>，业务可覆盖。
    /// </summary>
    private readonly IMessagePipelineObserver _observer;

    /// <summary>
    /// 注入观测实现。
    /// </summary>
    /// <param name="observer">观测实现，必须非空。</param>
    public TracingSendInterceptor(IMessagePipelineObserver observer)
    {
        _observer = observer;
    }

    /// <summary>
    /// Send 管线回调。
    /// </summary>
    /// <remarks>
    /// 进入观测作用域 → 执行 <paramref name="next"/> → 按成功/失败标记 → 返回响应或重抛异常。
    /// 响应对象原样返回，拦截器不加工、不替换。
    /// </remarks>
    /// <param name="request">请求体，只读。</param>
    /// <param name="next">
    /// 管线下一段。Send 语义需要把响应带回，类型为 <c>Func&lt;Task&lt;TResponse&gt;&gt;</c>。
    /// 必须调用一次且只调用一次。
    /// </param>
    /// <param name="context">生产者上下文，路径与取消令牌从中读取。</param>
    /// <returns>下游返回的响应体。</returns>
    public async Task<TResponse> OnHandle(
        TRequest request,
        Func<Task<TResponse>> next,
        IProducerContext context
    )
    {
        await using var scope = await _observer.EnterAsync(
            new MessagePipelineContext(
                MessageOperation.Send,
                context.Path ?? "<default>",
                typeof(TRequest),
                context.Path
            ),
            context.CancellationToken
        );

        try
        {
            var response = await next();

            scope.MarkSuccess();

            return response;
        }
        catch (Exception ex)
        {
            // 标记失败后原样抛出：请求-响应模式下调用方需要拿到异常，
            // 由调用方决定重试还是降级，拦截器不替它做决定。
            scope.MarkFailure(ex);

            throw;
        }
    }
}