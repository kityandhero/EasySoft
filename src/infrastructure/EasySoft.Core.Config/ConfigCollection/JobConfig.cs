using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// job 配置
/// </summary>
public class JobConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly JobConfig Instance = new();

    /// <summary>
    /// 最大线程数
    /// </summary>
    public string MaxThread { get; set; } = "1";

    /// <summary>
    /// 轮询时间间隔 (秒)
    /// </summary>
    public string TimeInterval { get; set; } = "600";

    /// <summary>
    /// 特定小时
    /// </summary>
    public string SpecifiedHour { get; set; } = "-1";

    /// <summary>
    /// 特定分钟 
    /// </summary>
    public string SpecifiedMinute { get; set; } = "-1";

    /// <summary>
    /// 特定秒钟
    /// </summary>
    public string SpecifiedSecond { get; set; } = "-1";

    /// <summary>
    /// 限时时间
    /// </summary>
    public string CurtailHour { get; set; } = "-1";
}