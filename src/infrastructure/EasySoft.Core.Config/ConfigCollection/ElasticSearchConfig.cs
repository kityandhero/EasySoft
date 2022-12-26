using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// ElasticSearch 配置
/// </summary>
public class ElasticSearchConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly ElasticSearchConfig Instance = new();

    /// <summary>
    /// ElasticSearch数据版本
    /// </summary>
    public string ElasticSearchDataVersion { get; set; } = "0";
}