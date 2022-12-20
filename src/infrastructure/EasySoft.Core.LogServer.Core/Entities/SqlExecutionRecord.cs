using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// SqlExecutionRecord
/// </summary>
public class SqlExecutionRecord : BaseEntity, ISqlExecutionRecord, IStatus, IOperate
{
    /// <inheritdoc />
    public string ExecuteGuid { get; set; } = UniqueIdAssist.CreateUUID();

    /// <inheritdoc />
    public string CommandString { get; set; } = "";

    /// <inheritdoc />
    public int ExecuteType { get; set; } = SqlExecuteType.Unknown.ToInt();

    /// <inheritdoc />
    public string ExecuteTypeSource { get; set; } = "";

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
    public int Channel { get; set; }

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;
}