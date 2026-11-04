namespace EasySoft.Core.Messaging.Abstractions;

/// <summary>
/// 链路观测契约。
/// </summary>
/// <remarks>
/// 拦截器只负责把传输层回调翻译成这里的调用；换传输层时拦截器随适配包一起换，观测实现不动。
/// </remarks>
public interface IMessagePipelineObserver
{
    /// <summary>
    /// 进入一次消息处理，返回作用域用于标记成功/失败并收尾。
    /// </summary>
    /// <param name="context">管线上下文。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    ValueTask<IMessageScope> EnterAsync(MessagePipelineContext context, CancellationToken cancellationToken);
}