using SqlExecuteType = EasySoft.UtilityTools.Standard.Enums.SqlExecuteType;

namespace EasySoft.Core.Sql.Entities;

/// <summary>
/// sql message
/// </summary>
public class SqlExecutionMessage : ISqlExecutionRecord
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
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;
}