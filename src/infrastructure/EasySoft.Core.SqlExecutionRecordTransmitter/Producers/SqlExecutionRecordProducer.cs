using EasySoft.UtilityTools.Standard.Entities.Implementations;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

/// <inheritdoc />
public class SqlExecutionRecordProducer : ISqlExecutionRecordProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// SqlExecutionRecordProducer
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public SqlExecutionRecordProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task<ISqlExecutionRecordExchange> SendAsync(
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
    )
    {
        var entity = new SqlExecutionRecordExchange
        {
            CommandString = commandString,
            ExecuteType = executeType,
            StackTraceSnippet = stackTraceSnippet,
            StartMilliseconds = startMilliseconds,
            DurationMilliseconds = durationMilliseconds,
            FirstFetchDurationMilliseconds = firstFetchDurationMilliseconds,
            Errored = errored,
            CollectMode = collectMode,
            DatabaseChannel = databaseChannel,
            Channel = _applicationChannel.GetChannel()
        };

        await _capPublisher.PublishAsync(
            TransmitterTopic.SqlExecutionRecordExchange,
            entity
        );

        return entity;
    }

    /// <inheritdoc />
    public async Task<ISqlExecutionRecord> SendAsync(ISqlExecutionRecord executionRecordExchange)
    {
        await _capPublisher.PublishAsync(
            TransmitterTopic.SqlExecutionRecordExchange,
            executionRecordExchange
        );

        return executionRecordExchange;
    }
}