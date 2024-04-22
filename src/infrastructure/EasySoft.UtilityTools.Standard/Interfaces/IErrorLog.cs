namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 错误日志
/// </summary>
public interface IErrorLog
{
    /// <summary>
    /// 创建人
    /// </summary>
    long OperatorId { get; set; }

    /// <summary>
    /// Url入口
    /// </summary>
    string Url { get; set; }

    /// <summary>
    /// 消息文本
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// 附属信息
    /// </summary>
    public string AncillaryInformation { get; set; }

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    string StackTrace { get; set; }

    /// <summary>
    /// 源代码信息
    /// </summary>
    string Source { get; set; }

    /// <summary>
    /// 场景描述
    /// </summary>
    string Scene { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    int Type { get; set; }

    /// <summary>
    /// 类型说明
    /// </summary>
    string TypeNote { get; set; }

    /// <summary>
    /// 重要程度
    /// </summary>
    int Degree { get; set; }

    /// <summary>
    /// 重要程度说明
    /// </summary>
    string DegreeNote { get; set; }

    /// <summary>
    /// 错误产生时是否发送通知
    /// </summary>
    int SendNotification { get; set; }

    /// <summary>
    /// 通知发送结果（成功/失败）
    /// </summary>
    int SendResult { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    DateTime SendTime { get; set; }

    /// <summary>
    /// Url头信息
    /// </summary>
    string Header { get; set; }

    /// <summary>
    /// Url参数
    /// </summary>
    string UrlParams { get; set; }

    /// <summary>
    /// Payload参数
    /// </summary>
    string PayloadParams { get; set; }

    /// <summary>
    /// Url表单参数
    /// </summary>
    string FormParams { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    string Host { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    int Port { get; set; }

    /// <summary>
    /// 自定义日志
    /// </summary>
    string CustomLog { get; set; }

    /// <summary>
    /// 自定义数据
    /// </summary>
    string CustomData { get; set; }

    /// <summary>
    /// 自定义数据类型
    /// </summary>
    int CustomDataType { get; set; }

    /// <summary>
    /// 解决状态
    /// </summary>
    int Resolve { get; set; }

    /// <summary>
    /// 异常类型的名称
    /// </summary>
    string ExceptionTypeName { get; set; }

    /// <summary>
    /// 异常类型的全名
    /// </summary>
    string ExceptionTypeFullName { get; set; }

    /// <summary>
    /// 解决状态说明
    /// </summary>
    string ResolveNote { get; set; }
}