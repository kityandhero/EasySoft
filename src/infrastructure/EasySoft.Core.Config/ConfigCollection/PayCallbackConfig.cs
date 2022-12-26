using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// PayCallbackConfig
/// </summary>
public class PayCallbackConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly PayCallbackConfig Instance = new();

    /// <summary>
    /// CallbackHost
    /// </summary>
    public string CallbackHost { get; set; } = "";
}