using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// 数据库配置
/// </summary>
public class DatabaseConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly DatabaseConfig Instance = new();

    /// <summary>
    /// 主数据库
    /// </summary>
    public string MainConnection { get; set; } = "";

    /// <summary>
    /// 从数据库
    /// </summary>
    public string MirrorConnection { get; set; } = "";

    /// <summary>
    /// 历史数据库
    /// </summary>
    public string HistoryConnection { get; set; } = "";

    /// <summary>
    /// 历史迁移异常数据库
    /// </summary>
    public string HistoryErrorConnection { get; set; } = "";
}