namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableInformation("Sql日志开关")]
[AdvanceTableMapper("channel_sql_log_switch")]
public class ChannelSqlLogSwitch : AbstractFunctionEntity<ChannelSqlLogSwitch>
{
    #region Properties

    [AdvanceColumnMapper("tag")]
    [AdvanceColumnLength(50)]
    public string Tag { get; set; } = "";

    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnMapper("key")]
    [AdvanceColumnLength(100)]
    public string Key { get; set; } = "";

    [AdvanceColumnInformation("值")]
    [AdvanceColumnMapper("value")]
    [AdvanceColumnNational]
    public string Value { get; set; } = "";

    #endregion Properties
}