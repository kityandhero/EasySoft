using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;

/// <summary>
/// 全局信息收集器。应用内任意程序集通过构造函数注入使用，生命周期为单例。
/// 实现内部通过 SMB 向 RabbitMQ 推送数据，接口本身不暴露任何传输层类型。
/// </summary>
public interface IRabbitMQInformationCollector
{
    /// <summary>
    /// 推送一条信息收集事件。失败时异常向上抛，调用方决定重试或降级。
    /// </summary>
    Task CollectAsync(RabbitMQInformationCollectedEvent message, CancellationToken ct = default);
}