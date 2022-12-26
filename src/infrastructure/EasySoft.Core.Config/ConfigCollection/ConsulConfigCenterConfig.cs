namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// consul配置中心设置
/// </summary>
public class ConsulConfigCenterConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly ConsulConfigCenterConfig Instance = new();

    /// <summary>
    /// 配置中心地址
    /// </summary>
    public string CenterAddress { get; set; } = "";
}