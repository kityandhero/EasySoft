using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Collector;

/// <summary>
/// 一般日志收集器
/// </summary>
public interface IGeneralLogCollector
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <returns>发送结果</returns>
    public bool Publish(string message);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="messageValueType">消息类型</param>
    /// <returns>发送结果</returns>
    public bool Publish(
        object message,
        CustomValueType messageValueType
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="message">消息体</param>
    /// <param name="messageValueType">消息类型</param>
    /// <param name="content">内容</param>
    /// <param name="contentValueType">内容类型</param>
    /// <returns>发送结果</returns>
    public bool Publish(
        object message,
        CustomValueType messageValueType,
        object content,
        CustomValueType contentValueType
    );
}