using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Models;

/// <summary>
/// FlagItem
/// </summary>
public class FlagItem : IFlagItem
{
    /// <inheritdoc />
    public string Key { get; set; } = "";

    /// <inheritdoc />
    public string Flag { get; set; } = "";

    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public int Availability { get; set; }
}