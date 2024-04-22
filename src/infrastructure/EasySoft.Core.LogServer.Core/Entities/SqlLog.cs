using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// SqlLog
/// </summary>
public class SqlLog : BaseEntity, ISqlLogStore
{
    #region Properties

    /// <inheritdoc />
    public string Flag { get; set; } = UniqueIdAssist.CreateUUID();

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
    public int Status { get; set; }

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    #endregion

    #region Methods

    /// <inheritdoc />
    public string GetId()
    {
        return Id.ToString();
    }

    /// <inheritdoc />
    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }

    #endregion
}