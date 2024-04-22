using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Models;

/// <inheritdoc />
public class NameValue : INameValue
{
    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public object? Value { get; set; } = "";
}