namespace EasySoft.Core.Sql.Enums;

/// <summary>
/// column transfer mode
/// </summary>
public enum ColumnTransferMode
{
    /// <summary>
    /// 包含表名
    /// </summary>
    [Description("包含表名")]
    ContainTableName = 10,

    /// <summary>
    /// 排除表名
    /// </summary>
    [Description("排除表名")]
    ExclusiveTableName = 20
}