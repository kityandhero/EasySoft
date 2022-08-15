using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class DatabaseConfig : IConfig
{
    public static readonly DatabaseConfig Instance = new();

    public string MainConnection { get; set; }

    public string MirrorConnection { get; set; }

    /// <summary>
    /// 历史数据库
    /// </summary>
    public string HistoryConnection { get; set; }

    /// <summary>
    /// 历史迁移异常数据库
    /// </summary>
    public string HistoryErrorConnection { get; set; }

    public DatabaseConfig()
    {
        MainConnection = "";
        MirrorConnection = "";
        HistoryConnection = "";
        HistoryErrorConnection = "";
    }
}