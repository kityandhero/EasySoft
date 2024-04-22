namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 执行日志开关
/// </summary>
[AdvanceTableInformation("执行日志开关")]
[AdvanceTableMapper("channel_execute_log_switch")]
public class ChannelExecuteLogSwitch : AbstractFunctionEntity<ChannelExecuteLogSwitch>
{
    #region Properties

    /// <summary>
    /// 唯一标记
    /// </summary>
    [AdvanceColumnInformation("唯一标记")]
    [AdvanceColumnMapper("tag")]
    [AdvanceColumnLength(50)]
    public string Tag { get; set; } = "";

    /// <summary>
    /// 标题
    /// </summary>
    [AdvanceColumnInformation("标题")]
    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    /// <summary>
    /// 键名
    /// </summary>
    [AdvanceColumnInformation("键名")]
    [AdvanceColumnMapper("key")]
    [AdvanceColumnLength(100)]
    public string Key { get; set; } = "";

    /// <summary>
    /// 键值
    /// </summary>
    [AdvanceColumnInformation("键值")]
    [AdvanceColumnMapper("value")]
    [AdvanceColumnNational]
    public string Value { get; set; } = "";

    #endregion Properties
}