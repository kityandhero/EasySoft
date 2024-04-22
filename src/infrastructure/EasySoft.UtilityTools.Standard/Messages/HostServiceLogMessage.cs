using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 驻留服务消息
/// </summary>
public class HostServiceLogMessage : IHostServiceLog, IQueueMessage, IIgnore
{
    #region Properties

    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string ServiceChannel { get; set; } = Channel.Unknown.ToValue();

    /// <summary>
    /// Changed
    /// </summary>
    public int Changed { get; set; }

    /// <summary>
    /// ChangeType
    /// </summary>
    public int ChangeType { get; set; }

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Channel.Unknown.ToValue();

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}