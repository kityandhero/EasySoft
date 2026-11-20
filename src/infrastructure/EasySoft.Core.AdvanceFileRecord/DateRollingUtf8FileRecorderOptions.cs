namespace EasySoft.Core.AdvanceFileRecord;

/// <summary>
/// 按日期滚动的UTF-8文件记录器配置
/// </summary>
public sealed class DateRollingUtf8FileRecorderOptions
{
    /// <summary>
    /// 文件路径模板，支持模板占位符 {Date:yyyyMMdd}
    /// </summary>
    /// <remarks>
    /// 示例：records/data_{Date:yyyyMMdd}.dat
    /// 滚动时机：日期变更时切换新文件
    /// </remarks>
    public string PathTemplate { get; set; } = string.Empty;

    /// <summary>
    /// 批量缓冲区总字节上限
    /// 缓冲区填满会自动触发刷盘
    /// 默认：128KB
    /// </summary>
    public int BatchBufferSize { get; set; } = 128 * 1024;

    /// <summary>
    /// 定时自动刷盘间隔
    /// 即使缓冲区没满，超时也会落地，防止数据长时间驻留内存丢失
    /// 默认：3秒
    /// </summary>
    public TimeSpan FlushInterval { get; set; } = TimeSpan.FromSeconds(3);

    /// <summary>
    /// FileStream 内部IO缓冲区大小
    /// 默认：64KB
    /// </summary>
    public int StreamBufferSize { get; set; } = 64 * 1024;

    /// <summary>
    /// 滚动日期是否使用本地时间
    /// false = UTC日期（推荐，跨时区统一归档）；true = 本机本地日期
    /// </summary>
    public bool UseLocalTimeForRolling { get; set; } = false;

    /// <summary>
    /// 启用临时文件+Rename原子落盘
    /// true：先写入 xxx.dat.tmp，刷盘完成后rename为正式文件名
    /// 优点：外部读取程序永远不会读到半截、截断的NDJSON/文本行
    /// 缺点：多一次rename操作；Windows下同名占用会失败
    /// false：直接Append追加到目标文件，最简模式
    /// </summary>
    public bool UseTempFileRename { get; set; } = false;

    #region 磁盘压力保护

    /// <summary>开启磁盘剩余空间保护</summary>
    public bool EnableDiskPressureProtection { get; set; } = false;

    /// <summary>磁盘最低保留字节阈值，低于该值丢弃新记录</summary>
    public long DiskMinFreeBytes { get; set; } = 500L * 1024 * 1024;

    #endregion

    #region 目录自维护（旧文件自动清理）

    /// <summary>
    /// 目录总容量上限（字节），0=关闭自动清理
    /// 当目录总文件大小超过该值，自动删除最早创建的文件
    /// </summary>
    public long MaxDirectorySizeBytes { get; set; } = 0;

    /// <summary>
    /// 目录扫描冷却间隔
    /// 每次刷盘成功才会尝试扫描；扫描后冷却，避免频繁遍历目录耗IO
    /// 默认：5分钟
    /// </summary>
    public TimeSpan ScanCooldown { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// 最少保留文件数量，保护底线
    /// 无论容量/时间超限，至少保留该数量文件，防止全部删空
    /// </summary>
    public int MinKeepFileCount { get; set; } = 3;

    /// <summary>
    /// 文件最大存活时长，null=不按时间淘汰
    /// 文件创建时间超过该时长，满足条件时删除
    /// 容量超限 OR 时间超限，任一条件触发即判定可删除
    /// </summary>
    public TimeSpan? MaxFileAge { get; set; }

    #endregion
}