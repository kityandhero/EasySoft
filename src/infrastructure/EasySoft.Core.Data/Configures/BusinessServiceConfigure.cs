using Masuit.Tools;

namespace EasySoft.Core.Data.Configures;

/// <summary>
/// 业务服务配置
/// </summary>
public static class BusinessServiceConfigure
{
    /// <summary>
    /// 业务服务定义程序集
    /// </summary>
    public static ISet<Assembly> BusinessServiceInterfaceAssemblies { get; }

    /// <summary>
    /// 业务服务实现程序集
    /// </summary>
    public static ISet<Assembly> BusinessServiceImplementationAssemblies { get; }

    /// <summary>
    /// 业务服务配置
    /// </summary>
    static BusinessServiceConfigure()
    {
        BusinessServiceInterfaceAssemblies = new HashSet<Assembly>();
        BusinessServiceImplementationAssemblies = new HashSet<Assembly>();
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