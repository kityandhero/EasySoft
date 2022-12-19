namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// 错误日志
/// </summary>
public interface IErrorLog : IChannel
{
    /// <summary>
    /// 创建人
    /// </summary>
    [Description("创建人")]
    long UserId { get; set; }

    /// <summary>
    /// Url入口
    /// </summary>
    [Description("Url入口")]
    string Url { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    [Description("消息描述")]
    string Message { get; set; }

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    [Description("堆栈跟踪")]
    string StackTrace { get; set; }

    /// <summary>
    /// 源代码信息
    /// </summary>
    [Description("源代码信息")]
    string Source { get; set; }

    /// <summary>
    /// 场景描述
    /// </summary>
    [Description("场景描述")]
    string Scene { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    int Type { get; set; }

    /// <summary>
    /// 重要程度
    /// </summary>
    [Description("重要程度")]
    int Degree { get; set; }

    /// <summary>
    /// Url头信息
    /// </summary>
    [Description("Url头信息")]
    string Header { get; set; }

    /// <summary>
    /// Url参数
    /// </summary>
    [Description("Url参数")]
    string UrlParams { get; set; }

    /// <summary>
    /// Payload参数
    /// </summary>
    [Description("Payload参数")]
    string PayloadParams { get; set; }

    /// <summary>
    /// Url表单参数
    /// </summary>
    [Description("Url表单参数")]
    string FormParams { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    [Description("域名")]
    string Host { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    [Description("端口")]
    int Port { get; set; }

    /// <summary>
    /// 自定义日志
    /// </summary>
    [Description("自定义日志")]
    string CustomLog { get; set; }

    /// <summary>
    /// 自定义数据
    /// </summary>
    [Description("自定义数据")]
    string CustomData { get; set; }

    /// <summary>
    /// 自定义数据类型
    /// </summary>
    [Description("自定义数据类型")]
    int CustomDataType { get; set; }

    /// <summary>
    /// 异常类型的名称
    /// </summary>
    [Description("异常类型的名称")]
    string ExceptionTypeName { get; set; }

    /// <summary>
    /// 异常类型的全名
    /// </summary>
    [Description("异常类型的全名")]
    string ExceptionTypeFullName { get; set; }
}