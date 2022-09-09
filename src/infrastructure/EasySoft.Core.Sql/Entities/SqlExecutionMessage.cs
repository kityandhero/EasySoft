namespace EasySoft.Core.Sql.Entities;

public class SqlExecutionMessage
{
    public string SqlExecutionMessageId { get; set; }

    public string CommandString { get; set; }

    public string ExecuteType { get; set; }

    public string StackTraceSnippet { get; set; }

    public double StartMilliseconds { get; set; }

    public double DurationMilliseconds { get; set; }

    public double FirstFetchDurationMilliseconds { get; set; }

    public int Errored { get; set; }

    public int TriggerChannel { get; set; }

    public int CollectMode { get; set; }

    public int Channel { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public long CreateUnixTime { get; set; }

    /// <summary>
    /// 创建人标识 
    /// </summary>
    public long CreateOperatorId { get; set; }

    public string DatabaseChannel { get; set; }

    public SqlExecutionMessage()
    {
        SqlExecutionMessageId = "";
        CommandString = "";
        ExecuteType = "";
        StackTraceSnippet = "";
        DatabaseChannel = "";
    }
}