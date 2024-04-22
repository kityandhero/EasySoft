namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// ISqlLog
/// </summary>
public interface ISqlLog
{
    /// <summary>
    /// 收集标记，用于辅助第三方工具标记其唯一性
    /// </summary>
    [Description("收集标记，用于辅助第三方工具标记其唯一性")]
    string Flag { get; set; }

    /// <summary>
    /// 命令文本
    /// </summary>
    [Description("命令文本")]
    string CommandString { get; set; }

    /// <summary>
    /// 执行类型
    /// </summary>
    [Description("执行类型")]
    string ExecuteType { get; set; }

    /// <summary>
    /// 堆栈跟踪代码段
    /// </summary>
    [Description("堆栈跟踪代码段")]
    string StackTraceSnippet { get; set; }

    /// <summary>
    /// StartMilliseconds
    /// </summary>
    [Description("StartMilliseconds")]
    decimal StartMilliseconds { get; set; }

    /// <summary>
    /// DurationMilliseconds
    /// </summary>
    [Description("DurationMilliseconds")]
    decimal DurationMilliseconds { get; set; }

    /// <summary>
    /// FirstFetchDurationMilliseconds
    /// </summary>
    [Description("FirstFetchDurationMilliseconds")]
    decimal FirstFetchDurationMilliseconds { get; set; }

    /// <summary>
    /// Errored
    /// </summary>
    [Description("Errored")]
    int Errored { get; set; }

    /// <summary>
    /// CollectMode
    /// </summary>
    [Description("CollectMode")]
    int CollectMode { get; set; }

    /// <summary>
    /// DatabaseChannel
    /// </summary>
    [Description("DatabaseChannel")]
    string DatabaseChannel { get; set; }
}