namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// IErrorLogPersistence
/// </summary>
public interface IErrorLogPersistence
{
    /// <summary>
    /// 创建人
    /// </summary>
    [Description("创建人")]
    public long UserId { get; set; }

    /// <summary>
    /// Url入口
    /// </summary>
    [Description("Url入口")]
    public string Url { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    [Description("消息描述")]
    public string Message { get; set; }

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    [Description("堆栈跟踪")]
    public string StackTrace { get; set; }

    /// <summary>
    /// 源代码信息
    /// </summary>
    [Description("源代码信息")]
    public string Source { get; set; }

    /// <summary>
    /// 场景描述
    /// </summary>
    [Description("场景描述")]
    public string Scene { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Description("类型")]
    public int Type { get; set; }

    /// <summary>
    /// 重要程度
    /// </summary>
    [Description("重要程度")]
    public int Degree { get; set; }

    /// <summary>
    /// Url头信息
    /// </summary>
    [Description("Url头信息")]
    public string Header { get; set; }

    /// <summary>
    /// Url参数
    /// </summary>
    [Description("Url参数")]
    public string UrlParams { get; set; }

    /// <summary>
    /// Payload参数
    /// </summary>
    [Description("Payload参数")]
    public string PayloadParams { get; set; }

    /// <summary>
    /// Url表单参数
    /// </summary>
    [Description("Url表单参数")]
    public string FormParams { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    [Description("域名")]
    public string Host { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    [Description("端口")]
    public int Port { get; set; }

    /// <summary>
    /// 自定义日志
    /// </summary>
    [Description("自定义日志")]
    public string CustomLog { get; set; }

    /// <summary>
    /// 自定义数据
    /// </summary>
    [Description("自定义数据")]
    public string CustomData { get; set; }

    /// <summary>
    /// 自定义数据类型
    /// </summary>
    [Description("自定义数据类型")]
    public int CustomDataType { get; set; }

    /// <summary>
    /// 异常类型的名称
    /// </summary>
    [Description("异常类型的名称")]
    public string ExceptionTypeName { get; set; }

    /// <summary>
    /// 异常类型的全名
    /// </summary>
    [Description("异常类型的全名")]
    public string ExceptionTypeFullName { get; set; }
}