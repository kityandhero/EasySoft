using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

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
    /// <param name="triggerChannel"></param>
    /// <param name="collectMode"></param>
    /// <param name="databaseChannel"></param>
    /// <returns></returns>
    Task<ISqlExecutionRecordExchange> SendAsync(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int triggerChannel,
        int collectMode,
        string databaseChannel
    );

    /// <summary>
    /// send
    /// </summary>
    /// <param name="executionRecordExchange"></param>
    /// <returns></returns>
    Task<ISqlExecutionRecordExchange> SendAsync(ISqlExecutionRecordExchange executionRecordExchange);
}