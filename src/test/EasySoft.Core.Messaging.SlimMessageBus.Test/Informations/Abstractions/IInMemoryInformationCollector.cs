using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;

public interface IInMemoryInformationCollector
{
    /// <summary>
    /// 推送一条信息收集事件。失败时异常向上抛，调用方决定重试或降级。
    /// </summary>
    Task CollectAsync(InMemoryInformationCollectedEvent message, CancellationToken ct = default);
}