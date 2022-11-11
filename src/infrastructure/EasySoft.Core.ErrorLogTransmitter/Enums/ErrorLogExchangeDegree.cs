namespace EasySoft.Core.ErrorLogTransmitter.Enums;

/// <summary>
/// 重要程度
/// </summary>
public enum ErrorLogExchangeDegree
{
    /// <summary>
    /// 一般记录
    /// </summary>
    [Description("一般记录")]
    Info = 100,

    /// <summary>
    /// 警告日志
    /// </summary>
    [Description("警告日志")]
    Warning = 200,

    /// <summary>
    /// 非严重错误
    /// </summary>
    [Description("非严重错误")]
    Error = 300,

    /// <summary>
    /// 严重错误
    /// </summary>
    [Description("严重错误")]
    Urgency = 400
}