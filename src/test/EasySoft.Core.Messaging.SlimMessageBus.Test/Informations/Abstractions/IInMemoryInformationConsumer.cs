using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;

/// <summary>
/// 内存队列版本的信息事件消费者。
/// 与 RabbitMQ 版本的 <c>IConsumer&lt;InformationCollectedEvent&gt;</c> 是两套独立的实现，
/// 互不引用，互不感知。消费者注册为 Scoped，每条消息在独立作用域内处理。
/// </summary>
public interface IInMemoryInformationConsumer
{
    /// <summary>
    /// 处理一条信息收集事件。异常由后台泵捕获并丢弃，不重试——内存队列不承诺可靠投递。
    /// </summary>
    Task HandleAsync(InMemoryInformationCollectedEvent message, CancellationToken ct);
}