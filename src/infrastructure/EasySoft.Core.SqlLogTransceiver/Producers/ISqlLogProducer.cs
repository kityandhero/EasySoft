namespace EasySoft.Core.SqlLogTransceiver.Producers;

/// <summary>
/// SQL日志发布器
/// </summary>
public interface ISqlLogProducer
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
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int collectMode,
        string databaseChannel,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="sqlLogMessage">消息体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>发送结果</returns>
    Task SendAsync(
        ISqlLogMessage sqlLogMessage,
        CancellationToken cancellationToken = default
    );
}