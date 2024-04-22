namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// SQL日志
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("sql_log")]
public class SqlLog : AbstractFunctionEntity<SqlLog>, ISqlLogStore
{
    #region Properties

    [AdvanceColumnInformation("收集标记，用于辅助第三方工具标记其唯一")]
    [AdvanceColumnMapper("flag")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string Flag { get; set; } = "";

    [AdvanceColumnInformation("命令文本")]
    [AdvanceColumnMapper("command_string")]
    [AdvanceColumnNational]
    public string CommandString { get; set; } = "";

    [AdvanceColumnInformation("执行类型")]
    [AdvanceColumnMapper("execute_type")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string ExecuteType { get; set; } = "";

    [AdvanceColumnInformation("堆栈跟踪代码段")]
    [AdvanceColumnMapper("stack_trace_snippet")]
    [AdvanceColumnNational]
    public string StackTraceSnippet { get; set; } = "";

    [AdvanceColumnInformation("开始毫秒")]
    [AdvanceColumnMapper("start_milliseconds")]
    public decimal StartMilliseconds { get; set; } = 0;

    [AdvanceColumnInformation("持续时间毫秒")]
    [AdvanceColumnMapper("duration_milliseconds")]
    public decimal DurationMilliseconds { get; set; } = 0;

    [AdvanceColumnInformation("第一次获取持续时间毫秒")]
    [AdvanceColumnMapper("first_fetch_duration_milliseconds")]
    public decimal FirstFetchDurationMilliseconds { get; set; } = 0;

    [AdvanceColumnInformation("错误")]
    [AdvanceColumnMapper("errored")]
    public int Errored { get; set; } = 0;

    [AdvanceColumnInformation("收集模式")]
    [AdvanceColumnMapper("collect_mode")]
    public int CollectMode { get; set; } = 0;

    [AdvanceColumnInformation("数据库通道")]
    [AdvanceColumnMapper("database_channel")]
    [AdvanceColumnNational]
    public string DatabaseChannel { get; set; } = "";

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