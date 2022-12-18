using Masuit.Tools;
using Masuit.Tools.Systems;

namespace EasySoft.Core.Data.Configures;

/// <summary>
/// 业务服务配置
/// </summary>
public static class BusinessServiceConfigure
{
    /// <summary>
    /// 业务服务定义程序集
    /// </summary>
    private static readonly ISet<Assembly> BusinessServiceInterfaceAssemblies = new ConcurrentHashSet<Assembly>();

    /// <summary>
    /// 业务服务实现程序集
    /// </summary>
    private static readonly ISet<Assembly> BusinessServiceImplementationAssemblies = new ConcurrentHashSet<Assembly>();

    /// <summary>
    /// GetBusinessServiceInterfaceAssemblies
    /// </summary>
    /// <returns></returns>
    public static ISet<Assembly> GetBusinessServiceInterfaceAssemblies()
    {
        return BusinessServiceInterfaceAssemblies;
    }

    /// <summary>
    /// 添加业务服务定义程序集
    /// </summary>
    /// <param name="assembly"></param>
    public static void AddBusinessServiceInterfaceAssembly(Assembly assembly)
    {
        BusinessServiceInterfaceAssemblies.Add(assembly);
    }

    /// <summary>
    /// 添加业务服务定义程序集
    /// </summary>
    /// <param name="assemblies"></param>
    public static void AddRangeBusinessServiceInterfaceAssemblies(IEnumerable<Assembly> assemblies)
    {
        BusinessServiceInterfaceAssemblies.AddRange(assemblies);
    }

    /// <summary>
    /// GetBusinessServiceImplementationAssemblies
    /// </summary>
    /// <returns></returns>
    public static ISet<Assembly> GetBusinessServiceImplementationAssemblies()
    {
        return BusinessServiceImplementationAssemblies;
    }

    /// <summary>
    /// 添加业务服务实现程序集
    /// </summary>
    /// <param name="assembly"></param>
    public static void AddBusinessServiceImplementationAssembly(Assembly assembly)
    {
        BusinessServiceImplementationAssemblies.Add(assembly);
    }

    /// <summary>
    /// 添加业务服务实现程序集
    /// </summary>
    /// <param name="assemblies"></param>
    public static void AddRangeBusinessServiceImplementationAssemblies(
        IEnumerable<Assembly> assemblies
    )
    {
        BusinessServiceImplementationAssemblies.AddRange(assemblies);
    }
}