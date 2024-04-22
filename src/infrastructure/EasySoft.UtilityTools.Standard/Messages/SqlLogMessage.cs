using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <inheritdoc />
public class SqlLogMessage : ISqlLogMessage
{
    #region Properties

    /// <inheritdoc />
    public string Flag { get; set; } = "";

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
    public string TriggerChannel { get; set; } = Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}