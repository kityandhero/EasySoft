using EasySoft.Core.Cap.CapConfigure;

namespace EasySoft.Core.Cap.Assists;

/// <summary>
/// Cap辅助
/// </summary>
public static class CapAssist
{
    private static readonly CapConfig Config = new();

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    public static CapConfig GetConfig()
    {
        return Config;
    }
}