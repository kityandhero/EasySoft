using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 一般日志队列信息
/// </summary>
public class GeneralLogMessage : IGeneralLogMessage
{
    #region Properties

    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public string Description { get; set; } = "";

    /// <inheritdoc />
    public string AncillaryInformation { get; set; } = "";

    /// <inheritdoc />
    public int MessageType { get; set; } = (int)CustomValueType.PlainValue;

    /// <inheritdoc />
    public string Content { get; set; } = "";

    /// <inheritdoc />
    public int ContentType { get; set; } = (int)CustomValueType.PlainValue;

    /// <inheritdoc />
    public int Type { get; set; } = (int)GeneralLogType.Common;

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}