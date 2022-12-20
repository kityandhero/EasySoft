using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Implementations;

namespace EasySoft.Core.AccessWayTransmitter.Entities;

/// <summary>
/// AccessWayExchange
/// </summary>
public class AccessWayExchange : BaseExchange, IAccessWayExchange
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
}