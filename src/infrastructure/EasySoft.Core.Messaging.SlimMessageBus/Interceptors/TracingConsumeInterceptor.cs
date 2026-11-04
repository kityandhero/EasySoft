using EasySoft.Core.Messaging.Abstractions;
using EasySoft.Core.Messaging.Observers;

namespace EasySoft.Core.Messaging.SlimMessageBus.Interceptors;

/// <summary>
/// Consume 链路拦截器。
/// </summary>
/// <remarks>
/// 实现 SlimMessageBus 的 <see cref="IConsumerInterceptor{TMessage}"/>。
/// 职责与 Publish/Send 拦截器一致：进入观测作用域、执行消费者、按成功/失败标记，
/// 不写业务逻辑，不吞异常。
/// 注意：消费侧拦截器的返回值与 <c>next</c> 类型是 <c>Task&lt;object&gt;</c> / <c>Func&lt;Task&lt;object&gt;&gt;</c>，
/// 与 Publish 的 <c>Func&lt;Task&gt;</c> 不同，也与 Send 的强类型响应不同。
/// </remarks>
/// <typeparam name="TMessage">
/// 被拦截的消息类型。SMB 运行时按消息类型解析闭合泛型实例，
/// 因此必须是消费者所处理的消息本身的类型。
/// </typeparam>
internal sealed class TracingConsumeInterceptor<TMessage> : IConsumerInterceptor<TMessage>
    where TMessage : notnull
{
    /// <summary>
    /// 观测实现。默认 <see cref="ActivityPipelineObserver"/>，业务可覆盖为自定义埋点。
    /// </summary>
    private readonly IMessagePipelineObserver _observer;

    /// <summary>注入观测实现。</summary>
    /// <param name="observer">观测实现，必须非空。</param>
    public TracingConsumeInterceptor(IMessagePipelineObserver observer)
    {
        _observer = observer;
    }

    /// <summary>
    /// Consume 管线回调。
    /// </summary>
    /// <remarks>
    /// 消费失败时标记后原样抛出，由传输层决定 ack/nack，本组件不做重试台账。
    /// </remarks>
    /// <param name="message">消息体，只读。</param>
    /// <param name="next">
    /// 管线下一段（消费者或后续拦截器）。类型为 <c>Func&lt;Task&lt;object&gt;&gt;</c>。
    /// 返回值通常被 SlimMessageBus 内部使用，调用方一般忽略。
    /// </param>
    /// <param name="context">
    /// 消费者上下文。<c>context.Path</c> 是消息来源路径；
    /// <c>context.CancellationToken</c> 是取消令牌。
    /// 消费侧不能通过拦截器改写消息头。
    /// </param>
    /// <returns>下游返回的管线结果对象。</returns>
    public async Task<object> OnHandle(
        TMessage message,
        Func<Task<object>> next,
        IConsumerContext context
    )
    {
        await using var scope = await _observer.EnterAsync(
            new MessagePipelineContext(
                MessageOperation.Consume,
                context.Path ?? "<default>",
                typeof(TMessage),
                context.Path
            ),
            context.CancellationToken
        );

        try
        {
            var result = await next();
            scope.MarkSuccess();
            return result;
        }
        catch (Exception ex)
        {
            // 标记失败后原样抛出：消费异常必须留给传输层感知，
            // 吞掉会让 broker 误以为消费成功，破坏 ack 语义。
            scope.MarkFailure(ex);
            throw;
        }
    }
}