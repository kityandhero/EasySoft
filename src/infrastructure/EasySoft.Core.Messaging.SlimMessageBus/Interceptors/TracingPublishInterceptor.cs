using EasySoft.Core.Messaging.Abstractions;
using EasySoft.Core.Messaging.Observers;

namespace EasySoft.Core.Messaging.SlimMessageBus.Interceptors;

/// <summary>
/// Publish 链路拦截器。
/// </summary>
/// <remarks>
/// SlimMessageBus <see cref="IPublishInterceptor{TMessage}"/> 实现。
/// 只把 SlimMessageBus 回调翻译成 <see cref="IMessagePipelineObserver"/> 调用，不写业务逻辑。
/// </remarks>
/// <typeparam name="TMessage">消息类型，SlimMessageBus 按类型解析该拦截器。</typeparam>
internal sealed class TracingPublishInterceptor<TMessage> : IPublishInterceptor<TMessage> where TMessage : notnull
{
    /// <summary>
    /// 观测实现。默认 <see cref="ActivityPipelineObserver"/>，业务可覆盖。
    /// </summary>
    private readonly IMessagePipelineObserver _observer;

    /// <summary>
    /// 构造拦截器。
    /// </summary>
    /// <remarks>
    /// 两个依赖都由当前单元的子容器注入，不会跨单元串用。
    /// </remarks>
    /// <param name="observer">观测实现，必须非空。</param>
    public TracingPublishInterceptor(IMessagePipelineObserver observer)
    {
        _observer = observer;
    }

    /// <summary>
    /// Publish 管线回调。
    /// </summary>
    /// <remarks>
    /// 进入观测作用域 → 执行 <paramref name="next"/> → 按成功/失败标记 → 释放作用域。
    /// 不捕获异常后吞掉，标记后原样抛出，交由上层或传输层处理。
    /// </remarks>
    /// <param name="message">消息体，只读，拦截器不应修改它。</param>
    /// <param name="next">管线下一段。</param>
    /// <param name="context">生产者上下文。路径、取消令牌、headers 均从中读取。</param>
    /// <returns>表示管线执行完成的任务。异常原样冒泡。</returns>
    public async Task OnHandle(
        TMessage message,
        Func<Task> next,
        IProducerContext context
    )
    {
        // 作用域用 using 包裹，保证无论成功还是异常都会释放 Activity/埋点资源。
        await using var scope = await _observer.EnterAsync(
            new MessagePipelineContext(
                MessageOperation.Publish,
                context.Path ?? "<default>",
                typeof(TMessage),
                context.Path
            ),
            context.CancellationToken
        );

        try
        {
            await next();

            scope.MarkSuccess();
        }
        catch (Exception ex)
        {
            // 先标记失败再重抛：吞掉异常会让传输层误以为投递成功，破坏 ack 语义。
            scope.MarkFailure(ex);

            throw;
        }
    }
}