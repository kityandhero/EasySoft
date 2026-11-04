namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Orders.Records;

/// <summary>
/// 订单支付完成事件，消费用。
/// </summary>
public sealed record OrderPaid(Guid OrderId);