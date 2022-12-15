namespace EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;

/// <summary>
/// 适用于 EntityFramework Core 实体配置
/// </summary>
public interface IEntityConfigure
{
    /// <summary>
    /// GetAssemblies
    /// </summary>
    /// <returns></returns>
    IEnumerable<Assembly> GetAssemblies();

    /// <summary>
    /// AddAssembly
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    IEntityConfigure AddAssembly(Assembly assembly);

    /// <summary>
    /// AddRangeAssemblies
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    IEntityConfigure AddRangeAssemblies(IEnumerable<Assembly> assemblies);

    /// <summary>
    /// OnModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    void OnModelCreating(ModelBuilder modelBuilder);
}