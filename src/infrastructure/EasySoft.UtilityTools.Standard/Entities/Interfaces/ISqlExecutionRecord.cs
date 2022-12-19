namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// ISqlExecutionRecord
/// </summary>
public interface ISqlExecutionRecord
{
    /// <summary>
    /// SqlExecutionRecordId
    /// </summary>
    [Description("SqlExecutionRecordId")]
    string SqlExecutionRecordId { get; set; }

    /// <summary>
    /// CommandString
    /// </summary>
    [Description("CommandString")]
    string CommandString { get; set; }

    /// <summary>
    /// ExecuteType
    /// </summary>
    [Description("ExecuteType")]
    string ExecuteType { get; set; }

    /// <summary>
    /// StackTraceSnippet
    /// </summary>
    [Description("StackTraceSnippet")]
    string StackTraceSnippet { get; set; }

    /// <summary>
    /// StartMilliseconds
    /// </summary>
    [Description("StartMilliseconds")]
    double StartMilliseconds { get; set; }

    /// <summary>
    /// DurationMilliseconds
    /// </summary>
    [Description("DurationMilliseconds")]
    double DurationMilliseconds { get; set; }

    /// <summary>
    /// FirstFetchDurationMilliseconds
    /// </summary>
    [Description("FirstFetchDurationMilliseconds")]
    double FirstFetchDurationMilliseconds { get; set; }

    /// <summary>
    /// Errored
    /// </summary>
    [Description("Errored")]
    int Errored { get; set; }

    /// <summary>
    /// TriggerChannel
    /// </summary>
    [Description("TriggerChannel")]
    int TriggerChannel { get; set; }

    /// <summary>
    /// CollectMode
    /// </summary>
    [Description("CollectMode")]
    int CollectMode { get; set; }

    /// <summary>
    /// DatabaseChannel
    /// </summary>
    [Description("DatabaseChannel")]
    string DatabaseChannel { get; set; }
}