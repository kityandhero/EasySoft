namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

/// <summary>
/// SqlLogProducer
/// </summary>
public interface ISqlLogProducer
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
    /// <param name="collectMode"></param>
    /// <param name="databaseChannel"></param>
    /// <returns></returns>
    Task<ISqlLogMessage> SendAsync(
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

    /// <summary>
    /// send
    /// </summary>
    /// <param name="logExchange"></param>
    /// <returns></returns>
    Task<ISqlLogMessage> SendAsync(ISqlLogMessage logExchange);
}