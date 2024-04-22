namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// name and value model
/// </summary>
public interface INameValue
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    object? Value { get; set; }
}