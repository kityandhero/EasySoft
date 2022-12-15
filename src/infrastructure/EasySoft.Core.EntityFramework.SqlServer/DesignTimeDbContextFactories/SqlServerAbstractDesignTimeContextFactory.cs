namespace EasySoft.Core.EntityFramework.SqlServer.DesignTimeDbContextFactories;

/// <summary>
/// SqlServerDesignTimeContextFactory 用于 migration
/// </summary>
public abstract class
    SqlServerAbstractDesignTimeContextFactory<TSqlServerContext> : IDesignTimeDbContextFactory<TSqlServerContext>
    where TSqlServerContext : SqlServerContext
{
    /// <summary>
    /// CreateDbContext
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public TSqlServerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlServerContext>();

        optionsBuilder.UseSqlServer(DatabaseConfigAssist.GetMainConnection());

        optionsBuilder.UseSnakeCaseNamingConvention();

        var entityConfigureAssemblies = ContextConfigure.EntityConfigureAssemblies;

        var entityConfigure = new EntityConfigure().AddRangeAssemblies(
            entityConfigureAssemblies
        );

        return typeof(TSqlServerContext).Create<TSqlServerContext>(optionsBuilder.Options, entityConfigure);
    }
}