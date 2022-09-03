using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.SqlExecutionRecordTransmitter.Entities;
using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;
using EasySoft.Core.SqlExecutionRecordTransmitter.MessageQuery;
using EasySoft.UtilityTools.Core.Channels;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Producers;

public class SqlExecutionRecordProducer : ISqlExecutionRecordProducer
{
    private readonly IQuery<ISqlExecutionRecordExchange> _query;

    private readonly IApplicationChannel _applicationChannel;

    public SqlExecutionRecordProducer(IApplicationChannel applicationChannel)
    {
        var factory = new QueryFactory();

        _query = factory.CreateQuery();

        _applicationChannel = applicationChannel;
    }

    private IQuery<ISqlExecutionRecordExchange> GetQuery()
    {
        return _query;
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

        GetQuery().Send(entity);

        return entity;
    }
}