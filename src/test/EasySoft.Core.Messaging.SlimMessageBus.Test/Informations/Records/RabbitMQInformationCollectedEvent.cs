namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

/// <summary>
/// 信息收集事件。全局单例产出的消息类型，不进入业务路由表。
/// </summary>
public sealed record RabbitMQInformationCollectedEvent(
    string Source,
    string Category,
    string Payload,
    DateTimeOffset Timestamp
);