namespace EasySoft.Core.EntityFramework.SqlServer.Contexts;

public class SqlServerContext : BasicContext
{
    public SqlServerContext(DbContextOptions options, IEntityConfigure entityConfigure) : base(
        options,
        entityConfigure
    )
    {
    }
}