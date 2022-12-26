using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// MongoConfig
/// </summary>
public class MongoConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly MongoConfig Instance = new();

    /// <summary>
    /// Connection
    /// </summary>
    public string Connection { get; set; } = "";

    /// <summary>
    /// Database
    /// </summary>
    public string Database { get; set; } = "";
}