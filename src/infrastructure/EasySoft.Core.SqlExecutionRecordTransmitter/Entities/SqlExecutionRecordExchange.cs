using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.Entities;

/// <summary>
/// SqlExecutionRecordExchange
/// </summary>
public class SqlExecutionRecordExchange : BaseExchange, ISqlExecutionRecordExchange
{
    /// <inheritdoc />
    public string CommandString { get; set; } = "";

    /// <inheritdoc />
    public string ExecuteType { get; set; } = "";

    /// <inheritdoc />
    public string StackTraceSnippet { get; set; } = "";

    /// <inheritdoc />
    public decimal StartMilliseconds { get; set; }

    /// <inheritdoc />
    public decimal DurationMilliseconds { get; set; }

    /// <inheritdoc />
    public decimal FirstFetchDurationMilliseconds { get; set; }

    /// <inheritdoc />
    public int Errored { get; set; }

    /// <inheritdoc />
    public int CollectMode { get; set; }

    /// <inheritdoc />
    public string DatabaseChannel { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; }
}