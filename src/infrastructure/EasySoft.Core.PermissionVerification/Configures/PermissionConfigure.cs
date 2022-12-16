using Masuit.Tools;

namespace EasySoft.Core.PermissionVerification.Configures;

/// <summary>
/// 权限扫描配置
/// </summary>
public static class PermissionConfigure
{
    /// <summary>
    /// 权限扫描定义程序集
    /// </summary>
    public static ISet<Assembly> ScanPermissionAssemblies { get; }

    /// <summary>
    /// 权限扫描配置
    /// </summary>
    static PermissionConfigure()
    {
        ScanPermissionAssemblies = new HashSet<Assembly>();

        var entryAssembly = Assembly.GetEntryAssembly();

        if (entryAssembly != null) ScanPermissionAssemblies.Add(entryAssembly);
    }

    /// <summary>
    /// 添加权限扫描定义程序集
    /// </summary>
    /// <param name="assembly"></param>
    public static void AddScanPermissionAssembly(Assembly assembly)
    {
        ScanPermissionAssemblies.Add(assembly);
    }

    /// <summary>
    /// 添加权限扫描定义程序集
    /// </summary>
    /// <param name="assemblies"></param>
    public static void AddRangeScanPermissionAssemblies(IEnumerable<Assembly> assemblies)
    {
        ScanPermissionAssemblies.AddRange(assemblies);
    }
}