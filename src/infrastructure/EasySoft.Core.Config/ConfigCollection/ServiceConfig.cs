using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// 
/// </summary>
public class ServiceConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly ServiceConfig Instance = new();

    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix { get; set; } = "";
}