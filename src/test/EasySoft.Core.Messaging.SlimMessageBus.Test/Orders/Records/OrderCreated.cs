namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Records;

/// <summary>
/// 订单创建事件，发布用。
/// </summary>
public sealed record OrderCreated(Guid OrderId, decimal Amount);