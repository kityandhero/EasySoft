using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// 开发配置
/// </summary>
public class DevelopConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly DevelopConfig Instance = new();

    /// <summary>
    /// 开发模式
    /// </summary>
    public string DevelopMode { get; set; } = "0";
}