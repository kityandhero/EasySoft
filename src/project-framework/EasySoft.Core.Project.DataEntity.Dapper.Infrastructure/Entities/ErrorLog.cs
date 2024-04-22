namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 错误日志
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("error_log")]
public class ErrorLog : AbstractFunctionEntity<ErrorLog>, IErrorLogStore
{
    #region Properties

    [AdvanceColumnInformation("创建人")]
    [AdvanceColumnMapper("user_id")]
    public long OperatorId { get; set; } = 0;

    [AdvanceColumnInformation("Url入口")]
    [AdvanceColumnMapper("url")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string Url { get; set; } = "";

    [AdvanceColumnInformation("消息文本")]
    [AdvanceColumnMapper("message")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string Message { get; set; } = "";

    [AdvanceColumnInformation("消息描述")]
    [AdvanceColumnMapper("description")]
    [AdvanceColumnNational]
    public string Description { get; set; } = "";

    [AdvanceColumnInformation("附属信息")]
    [AdvanceColumnMapper("ancillary_information")]
    [AdvanceColumnNational]
    public string AncillaryInformation { get; set; } = "";

    [AdvanceColumnInformation("堆栈跟踪")]
    [AdvanceColumnMapper("stack_trace")]
    [AdvanceColumnNational]
    public string StackTrace { get; set; } = "";

    [AdvanceColumnInformation("源代码信息")]
    [AdvanceColumnMapper("source")]
    [AdvanceColumnNational]
    public string Source { get; set; } = "";

    [AdvanceColumnInformation("场景描述")]
    [AdvanceColumnMapper("scene")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Scene { get; set; } = "";

    [AdvanceColumnInformation("类型")]
    [AdvanceColumnMapper("type")]
    public int Type { get; set; } = (int)ErrorLogType.DataError;

    [AdvanceColumnInformation("类型说明")]
    [AdvanceColumnMapper("type_note")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string TypeNote { get; set; } = ErrorLogType.DataError
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    [AdvanceColumnInformation("重要程度")]
    [AdvanceColumnMapper("degree")]
    public int Degree { get; set; } = (int)ErrorLogDegree.Info;

    [AdvanceColumnInformation("重要程度说明")]
    [AdvanceColumnMapper("degree_note")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string DegreeNote { get; set; } = ErrorLogDegree.Info
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    [AdvanceColumnInformation("错误产生时是否发送通知")]
    [AdvanceColumnMapper("send_notification")]
    public int SendNotification { get; set; } = 0;

    [AdvanceColumnInformation("通知发送结果（成功/失败")]
    [AdvanceColumnMapper("send_result")]
    public int SendResult { get; set; } = 0;

    [AdvanceColumnInformation("发送时间")]
    [AdvanceColumnMapper("send_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime SendTime { get; set; } = ConstCollection.DbDefaultDateTime;

    [AdvanceColumnInformation("Url头信息")]
    [AdvanceColumnMapper("header")]
    [AdvanceColumnNational]
    public string Header { get; set; } = "";

    [AdvanceColumnInformation("Url参数")]
    [AdvanceColumnMapper("url_params")]
    [AdvanceColumnNational]
    public string UrlParams { get; set; } = "";

    [AdvanceColumnInformation("Payload参数")]
    [AdvanceColumnMapper("payload_params")]
    [AdvanceColumnNational]
    public string PayloadParams { get; set; } = "";

    [AdvanceColumnInformation("Url表单参数")]
    [AdvanceColumnMapper("form_params")]
    [AdvanceColumnNational]
    public string FormParams { get; set; } = "";

    [AdvanceColumnInformation("域名")]
    [AdvanceColumnMapper("host")]
    [AdvanceColumnLength(500)]
    [AdvanceColumnNational]
    public string Host { get; set; } = "";

    [AdvanceColumnInformation("端口")]
    [AdvanceColumnMapper("port")]
    public int Port { get; set; } = 0;

    [AdvanceColumnInformation("自定义日志")]
    [AdvanceColumnMapper("custom_log")]
    [AdvanceColumnNational]
    public string CustomLog { get; set; } = "";

    [AdvanceColumnInformation("自定义数据")]
    [AdvanceColumnMapper("custom_data")]
    [AdvanceColumnNational]
    public string CustomData { get; set; } = "";

    [AdvanceColumnInformation("自定义数据类型")]
    [AdvanceColumnMapper("custom_data_type")]
    public int CustomDataType { get; set; } = (int)CustomValueType.PlainValue;

    [AdvanceColumnInformation("解决状态")]
    [AdvanceColumnMapper("resolve")]
    public int Resolve { get; set; } = (int)ErrorLogResolve.Unresolved;

    [AdvanceColumnInformation("解决状态说明")]
    [AdvanceColumnMapper("resolve_note")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string ResolveNote { get; set; } = ErrorLogResolve.Unresolved
        .GetCustomAttribute<DescriptionAttribute>()
        ?.Description ?? "";

    [AdvanceColumnInformation("异常类型的名称")]
    [AdvanceColumnMapper("exception_type_name")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string ExceptionTypeName { get; set; } = "";

    [AdvanceColumnInformation("异常类型的全名")]
    [AdvanceColumnMapper("exception_type_full_name")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string ExceptionTypeFullName { get; set; } = "";

    [AdvanceColumnInformation("触发渠道码")]
    [AdvanceColumnMapper("trigger_channel")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = "";

    #endregion Properties
}