namespace EasySoft.Core.Sql.Builders;

/// <summary>
/// AdvanceSqlBuilder
/// </summary>
public class AdvanceSqlBuilder
{
    /// <summary>
    /// Sql
    /// </summary>
    public string Sql { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public AdvanceSqlBuilder()
    {
        Sql = "";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sql"></param>
    public AdvanceSqlBuilder(string sql)
    {
        Sql = sql;
    }
}