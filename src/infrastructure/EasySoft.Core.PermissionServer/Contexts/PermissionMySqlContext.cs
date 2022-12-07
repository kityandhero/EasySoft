namespace EasySoft.Core.PermissionServer.Contexts;

public class PermissionMySqlContext : MySqlContext
{
    public PermissionMySqlContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options, entityConfigure)
    {
    }
}