using EasySoft.Core.Infrastructure.Transmitters;
using EasySoft.Core.SqlExecutionRecordTransmitter.Entities;
using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

public class SqlExecutionRecordProducer : ISqlExecutionRecordProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    public SqlExecutionRecordProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

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
    )
    {
        var entity = new SqlExecutionRecordExchange
        {
            SqlExecutionRecordId = sqlExecutionRecordId,
            CommandString = commandString,
            ExecuteType = executeType,
            StackTraceSnippet = stackTraceSnippet,
            StartMilliseconds = startMilliseconds,
            DurationMilliseconds = durationMilliseconds,
            FirstFetchDurationMilliseconds = firstFetchDurationMilliseconds,
            Errored = errored,
            TriggerChannel = triggerChannel,
            CollectMode = collectMode,
            DatabaseChannel = databaseChannel,
            Channel = _applicationChannel.GetChannel()
        };

        _capPublisher.Publish(TransmitterTopic.SqlExecutionRecordExchange, entity);

        return entity;
    }
}