using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Contexts;

namespace EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.DesignTimeDbContextFactories;

public class DesignTimeContextFactory : SqlServerAbstractDesignTimeContextFactory<DataMigrationContext>
{
    protected override ISet<Assembly> GetEntityConfigureAssemblies()
    {
        return new HashSet<Assembly>
        {
            typeof(User).Assembly,
            typeof(RoleGroup).Assembly
        };
    }
}