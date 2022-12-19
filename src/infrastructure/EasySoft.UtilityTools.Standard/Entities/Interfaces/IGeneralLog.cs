namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// 一般日志持久信息
/// </summary>
public interface IGeneralLog
{
    /// <summary>
    /// 消息描述
    /// </summary>
    [Description("消息描述")]
    string Message { get; set; }

    /// <summary>
    /// 消息描述数据类型  
    /// </summary>
    [Description("消息描述数据类型")]
    int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [Description("消息内容")]
    string Content { get; set; }

    /// <summary>
    /// 消息内容数据类型
    /// </summary>
    [Description("消息内容数据类型")]
    int ContentType { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    [Description("消息类型")]
    int Type { get; set; }
}