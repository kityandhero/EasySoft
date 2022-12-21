using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;
using SqlExecuteType = EasySoft.UtilityTools.Standard.Enums.SqlExecuteType;

namespace EasySoft.UtilityTools.Standard.Entities.Implements;

/// <summary>
/// SqlExecutionRecordExchange
/// </summary>
public class SqlExecutionRecordExchange : BaseExchange, ISqlExecutionRecordExchange
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
    public int Ignore { get; set; }
}