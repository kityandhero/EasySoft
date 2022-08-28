using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

public interface ISqlExecutionRecordProducer
{
    public ISqlExecutionRecordExchange Send(
        string sqlExecutionRecordId,
        string commandString,
        string executeType,
        string stackTraceSnippet,
        double startMilliseconds,
        double durationMilliseconds,
        double firstFetchDurationMilliseconds,
        int errored,
        int triggerChannel,
        int collectMode,
        string databaseChannel
    );
}