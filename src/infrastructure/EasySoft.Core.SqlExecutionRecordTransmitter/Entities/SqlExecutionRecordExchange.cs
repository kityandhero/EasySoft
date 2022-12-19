using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Entities;

/// <summary>
/// SqlExecutionRecordExchange
/// </summary>
public class SqlExecutionRecordExchange : BaseExchange, ISqlExecutionRecordExchange
{
    /// <inheritdoc />
    public string SqlExecutionRecordId { get; set; } = "";

    /// <inheritdoc />
    public string CommandString { get; set; } = "";

    /// <inheritdoc />
    public string ExecuteType { get; set; } = "";

    /// <inheritdoc />
    public string StackTraceSnippet { get; set; } = "";

    /// <inheritdoc />
    public double StartMilliseconds { get; set; }

    /// <inheritdoc />
    public double DurationMilliseconds { get; set; }

    /// <inheritdoc />
    public double FirstFetchDurationMilliseconds { get; set; }

    /// <inheritdoc />
    public int Errored { get; set; }

    /// <inheritdoc />
    public int TriggerChannel { get; set; }

    /// <inheritdoc />
    public int CollectMode { get; set; }

    /// <inheritdoc />
    public string DatabaseChannel { get; set; } = "";
}