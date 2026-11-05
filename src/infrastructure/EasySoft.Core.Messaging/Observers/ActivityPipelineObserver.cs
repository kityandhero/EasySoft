using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.Messaging.Observers;

/// <summary>
/// 默认观测实现：写入 <see cref="ActivitySource"/>，兼容 OpenTelemetry。
/// </summary>
/// <remarks>
/// 业务注入此门面，不感知目标单元、vhost、连接；路由由配置 + 启动校验保障。
/// 运行期不做回退，路由缺失直接抛。
/// </remarks>
public sealed class ActivityPipelineObserver : IMessagePipelineObserver
{
    /// <summary>
    /// 共享的 ActivitySource。进程内只建一份，避免重复注册监听者。
    /// </summary>
    private static readonly ActivitySource Source = new("Kernel.Messaging");

    /// <inheritdoc />
    public ValueTask<IMessageScope> EnterAsync(MessagePipelineContext context, CancellationToken ct)
    {
        // Consume 用 Consumer 语义，Publish/Send 用 Producer 语义，
        // 便于 OTel 后端按消息方向串链路。
        var activity = Source.StartActivity(
            $"{context.Operation} {context.MessageType.Name}",
            context.Operation == MessageOperation.Consume ? ActivityKind.Consumer : ActivityKind.Producer
        );

        activity?.SetTag("messaging.operation", context.Operation.ToString());
        activity?.SetTag("messaging.message_type", context.MessageType.FullName);
        activity?.SetTag("messaging.destination", context.Destination);

        return ValueTask.FromResult<IMessageScope>(new ActivityScope(activity));
    }

    /// <summary>
    /// Activity 作用域。仅持有 Activity 引用，不产生业务副作用。
    /// </summary>
    private sealed class ActivityScope : IMessageScope
    {
        /// <summary>
        /// 可能为 null（无监听者时 StartActivity 返回 null），所有调用都做空判断。
        /// </summary>
        private readonly Activity _activity;

        /// <summary>
        /// 包装一个 Activity 实例（可为 null）。
        /// </summary>
        /// <param name="activity">底层 Activity。</param>
        public ActivityScope(Activity activity)
        {
            _activity = activity;
        }

        /// <inheritdoc />
        public void MarkSuccess()
        {
            _activity?.SetStatus(ActivityStatusCode.Ok);
        }

        /// <inheritdoc />
        public void MarkFailure(Exception ex)
        {
            _activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
        }

        /// <summary>
        /// 释放 Activity。由拦截器在 finally 或 using 中调用。
        /// </summary>
        public ValueTask DisposeAsync()
        {
            _activity?.Dispose();

            return ValueTask.CompletedTask;
        }
    }
}