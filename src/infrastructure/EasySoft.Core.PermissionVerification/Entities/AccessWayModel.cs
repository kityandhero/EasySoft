namespace EasySoft.Core.PermissionVerification.Entities;

/// <summary>
/// AccessWayModel
/// </summary>
public class AccessWayModel : IAccessWay
{
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
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();
}