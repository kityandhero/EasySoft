namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 操作日志
/// </summary>
[AdvanceTableMapper("operation_log")]
public class OperationLog : AbstractFunctionEntity<OperationLog>
{
    #region Properties

    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnMapper("content")]
    [AdvanceColumnNational]
    public string Content { get; set; } = "";

    [AdvanceColumnMapper("content_type")]
    public int ContentType { get; set; } = 0;

    [AdvanceColumnMapper("table_name")]
    [AdvanceColumnLength(50)]
    public string TableName { get; set; } = "";

    [AdvanceColumnMapper("primary_key")]
    public long PrimaryKeyValue { get; set; } = 0;

    #endregion Properties
}