namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// ISqlExecutionRecord
/// </summary>
public interface ISqlExecutionRecord : IChannel, IIp, ICreate
{
    /// <summary>
    /// CommandString
    /// </summary>
    [Description("CommandString")]
    string ExecuteGuid { get; set; }

    /// <summary>
    /// CommandString
    /// </summary>
    [Description("CommandString")]
    string CommandString { get; set; }

    /// <summary>
    /// ExecuteType
    /// </summary>
    [Description("ExecuteType")]
    int ExecuteType { get; set; }

    /// <summary>
    /// ExecuteTypeSource
    /// </summary>
    [Description("ExecuteTypeSource")]
    string ExecuteTypeSource { get; set; }

    /// <summary>
    /// StackTraceSnippet
    /// </summary>
    [Description("StackTraceSnippet")]
    string StackTraceSnippet { get; set; }

    /// <summary>
    /// StartMilliseconds
    /// </summary>
    [Description("StartMilliseconds")]
    decimal StartMilliseconds { get; set; }

    /// <summary>
    /// DurationMilliseconds
    /// </summary>
    [Description("DurationMilliseconds")]
    decimal DurationMilliseconds { get; set; }

    /// <summary>
    /// FirstFetchDurationMilliseconds
    /// </summary>
    [Description("FirstFetchDurationMilliseconds")]
    decimal FirstFetchDurationMilliseconds { get; set; }

    /// <summary>
    /// Errored
    /// </summary>
    [Description("Errored")]
    int Errored { get; set; }

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