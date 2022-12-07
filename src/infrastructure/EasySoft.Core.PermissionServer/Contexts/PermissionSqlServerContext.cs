namespace EasySoft.Core.PermissionServer.Contexts;

public class PermissionSqlServerContext : SqlServerContext
{
    public PermissionSqlServerContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }
}