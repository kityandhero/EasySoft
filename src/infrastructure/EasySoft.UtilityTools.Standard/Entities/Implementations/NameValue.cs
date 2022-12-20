using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.UtilityTools.Standard.Entities.Implementations;

/// <inheritdoc />
public class NameValue : INameValue
{
    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public object? Value { get; set; } = "";
}