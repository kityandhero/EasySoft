namespace EasySoft.Core.EntityFramework.MySql.DesignTimeDbContextFactories;

/// <summary>
/// MySqlDesignTimeContextFactory 用于 migration
/// </summary>
public abstract class MySqlAbstractDesignTimeContextFactory<TMySqlContext> : IDesignTimeDbContextFactory<TMySqlContext>
    where TMySqlContext : MySqlContext
{
    /// <summary>
    /// CreateDbContext
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public TMySqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MySqlContext>();

        optionsBuilder.UseMySql(
            DatabaseConfigAssist.GetMainConnection(),
            ServerVersion.AutoDetect(DatabaseConfigAssist.GetMainConnection())
        );

        optionsBuilder.UseSnakeCaseNamingConvention();

        var entityConfigureAssemblies = ContextConfigure.EntityConfigureAssemblies;

        var entityConfigure = new EntityConfigure().AddRangeAssemblies(
            entityConfigureAssemblies
        );

        return typeof(TMySqlContext).Create<TMySqlContext>(optionsBuilder.Options, entityConfigure);
    }
}