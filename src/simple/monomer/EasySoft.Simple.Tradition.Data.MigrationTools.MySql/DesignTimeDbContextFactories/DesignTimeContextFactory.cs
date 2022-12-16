using EasySoft.Simple.Tradition.Data.MigrationTools.MySql.Contexts;

namespace EasySoft.Simple.Tradition.Data.MigrationTools.MySql.DesignTimeDbContextFactories;

public class DesignTimeContextFactory : MySqlAbstractDesignTimeContextFactory<DataMigrationContext>
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