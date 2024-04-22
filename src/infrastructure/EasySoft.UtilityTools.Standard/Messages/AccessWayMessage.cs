using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Messages;

/// <summary>
/// 模块传输消息
/// </summary>
public class AccessWayMessage : IAccessWayMessage
{
    #region Properties

    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string GuidTag { get; set; } = "";

    /// <inheritdoc />
    public string RelativePath { get; set; } = "";

    /// <inheritdoc />
    public int RelativePathLevel { get; set; }

    /// <inheritdoc />
    public string RelativeParentPath { get; set; } = "";

    /// <inheritdoc />
    public int RelativeParentPathLevel { get; set; }

    /// <inheritdoc />
    public string Expand { get; set; } = "";

    /// <inheritdoc />
    public string ResultType { get; set; } = "";

    /// <inheritdoc />
    public string Group { get; set; } = "";

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public int Ignore { get; set; } = Whether.No.ToInt();

    #endregion
}