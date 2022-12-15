namespace EasySoft.Core.EntityFramework.SqlServer.Contexts;

/// <summary>
/// SqlServerContext
/// </summary>
public class SqlServerContext : BasicContext
{
    /// <summary>
    /// SqlServerContext
    /// </summary>
    /// <param name="options"></param>
    /// <param name="entityConfigure"></param>
    public SqlServerContext(DbContextOptions options, IEntityConfigure entityConfigure) : base(
        options,
        entityConfigure
    )
    {
    }
}