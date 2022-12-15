using EasySoft.Simple.Tradition.Data.Contexts;
using EasySoft.Simple.Tradition.Data.Entities;

namespace EasySoft.Simple.Tradition.Data.DesignTimeDbContextFactories;

public class SqlServerAbstractContextFactory : SqlServerAbstractDesignTimeContextFactory<SqlServerDataContext>
{
    protected override ISet<Assembly> GetEntityConfigureAssemblies()
    {
        return new HashSet<Assembly>() { typeof(User).Assembly };
    }
}