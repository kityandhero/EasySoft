namespace EasySoft.UtilityTools.Standard.Collector;

/// <summary>
/// SQL日志收集器
/// </summary>
public interface ISqlLogCollector
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="commandString"></param>
    /// <param name="executeType"></param>
    /// <param name="stackTraceSnippet"></param>
    /// <param name="startMilliseconds"></param>
    /// <param name="durationMilliseconds"></param>
    /// <param name="firstFetchDurationMilliseconds"></param>
    /// <param name="errored"></param>
    /// <param name="collectMode"></param>
    /// <param name="databaseChannel"></param>
    /// <returns>发送结果</returns>
    public bool Publish(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int collectMode,
        string databaseChannel
    );
}