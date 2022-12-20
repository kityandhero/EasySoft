namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

/// <summary>
/// SqlExecutionRecordProducer
/// </summary>
public interface ISqlExecutionRecordProducer
{
    /// <summary>
    /// send
    /// </summary>
    /// <param name="commandString"></param>
    /// <param name="executeType"></param>
    /// <param name="stackTraceSnippet"></param>
    /// <param name="startMilliseconds"></param>
    /// <param name="durationMilliseconds"></param>
    /// <param name="firstFetchDurationMilliseconds"></param>
    /// <param name="errored"></param>
    /// <param name="channel"></param>
    /// <param name="collectMode"></param>
    /// <param name="databaseChannel"></param>
    /// <returns></returns>
    Task<ISqlExecutionRecordExchange> SendAsync(
        string commandString,
        int executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int channel,
        int collectMode,
        string databaseChannel
    );

    /// <summary>
    /// send
    /// </summary>
    /// <param name="executionRecordExchange"></param>
    /// <returns></returns>
    Task<ISqlExecutionRecord> SendAsync(ISqlExecutionRecord executionRecordExchange);
}