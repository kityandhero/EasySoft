using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Entities;

public class SqlExecutionRecordExchange : BaseExchange, ISqlExecutionRecordExchange
{
    public string SqlExecutionRecordId { get; set; }

    public string CommandString { get; set; }

    public string ExecuteType { get; set; }

    public string StackTraceSnippet { get; set; }

    public double StartMilliseconds { get; set; }

    public double DurationMilliseconds { get; set; }

    public double FirstFetchDurationMilliseconds { get; set; }

    public int Errored { get; set; }

    public int TriggerChannel { get; set; }

    public int CollectMode { get; set; }

    public string DatabaseChannel { get; set; }

    public SqlExecutionRecordExchange()
    {
        SqlExecutionRecordId = "";
        CommandString = "";
        ExecuteType = "";
        StackTraceSnippet = "";
        DatabaseChannel = "";
    }
}