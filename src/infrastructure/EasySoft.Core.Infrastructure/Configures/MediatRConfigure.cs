using Masuit.Tools;

namespace EasySoft.Core.Infrastructure.Configures;

/// <summary>
/// MediatRConfigure
/// </summary>
public static class MediatRConfigure
{
    /// <summary>
    /// Assemblies
    /// </summary>
    private static readonly ISet<Assembly> Assemblies = new ConcurrentHashSet<Assembly>();

    static MediatRConfigure()
    {
        AddAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// GetAssemblies
    /// </summary>
    public static ISet<Assembly> GetAssemblies()
    {
        return Assemblies;
    }

    /// <summary>
    /// AddAssembly
    /// </summary>
    /// <param name="assembly"></param>
    public static void AddAssembly(Assembly assembly)
    {
        Assemblies.Add(assembly);
    }

    /// <summary>
    /// AddRangeAssemblies
    /// </summary>
    /// <param name="assemblies"></param>
    public static void AddRangeAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies.AddRange(assemblies);
    }
}