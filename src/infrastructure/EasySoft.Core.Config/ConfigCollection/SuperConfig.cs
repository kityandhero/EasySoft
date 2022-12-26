using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// 超级账户配置
/// </summary>
public class SuperConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly SuperConfig Instance = new();

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = "";
}