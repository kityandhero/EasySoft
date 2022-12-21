using System.Reflection;
using EasySoft.Core.EntityFramework.EntityConfigures.Implements;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.EntityFramework.MySql.DesignTimeDbContextFactories;

/// <summary>
/// MySqlDesignTimeContextFactory 用于 migration
/// </summary>
public abstract class MySqlAbstractDesignTimeContextFactory<TMySqlContext> : IDesignTimeDbContextFactory<TMySqlContext>
    where TMySqlContext : MySqlContext
{
    /// <summary>
    /// GetEntityConfigureAssemblies
    /// </summary>
    /// <returns></returns>
    protected abstract ISet<Assembly> GetEntityConfigureAssemblies();

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

        var entityConfigure = new EntityConfigure().AddRangeAssemblies(
            GetEntityConfigureAssemblies()
        );

        return typeof(TMySqlContext).Create<TMySqlContext>(optionsBuilder.Options, entityConfigure);
    }
}