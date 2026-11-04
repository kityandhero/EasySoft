namespace EasySoft.Core.Messaging;

/// <summary>
/// 消息管线中的阶段。
/// </summary>
public enum MessageOperation
{
    /// <summary>
    /// 发布（点对多）。。
    /// </summary>
    Publish,

    /// <summary>
    /// 请求/响应发送（点对点）。
    /// </summary>
    Send,

    /// <summary>
    /// 消费。
    /// </summary>
    Consume
}