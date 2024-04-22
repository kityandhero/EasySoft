using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

/// <inheritdoc />
public class SqlLogProducer : ISqlLogProducer
{
    private readonly ICapPublisher _capPublisher;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// SqlLogProducer
    /// </summary>
    /// <param name="capPublisher"></param>
    /// <param name="applicationChannel"></param>
    public SqlLogProducer(ICapPublisher capPublisher, IApplicationChannel applicationChannel)
    {
        _capPublisher = capPublisher;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public async Task<ISqlLogMessage> SendAsync(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int collectMode,
        string databaseChannel
    )
    {
        var entity = new SqlLogMessage
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
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        await _capPublisher.PublishAsync(
            TransmitterTopic.SqlLogMessage,
            entity
        );

        return entity;
    }

    /// <inheritdoc />
    public async Task<ISqlLogMessage> SendAsync(ISqlLogMessage logExchange)
    {
        await _capPublisher.PublishAsync(
            TransmitterTopic.SqlLogMessage,
            logExchange
        );

        return logExchange;
    }
}