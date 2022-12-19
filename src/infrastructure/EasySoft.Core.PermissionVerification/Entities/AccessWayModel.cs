using EasySoft.UtilityTools.Standard.Entities.Interfaces;

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
    public string Expand { get; set; } = "";

    /// <inheritdoc />
    public string Group { get; set; } = "";

    /// <inheritdoc />
    public int Channel { get; set; } = 0;
}