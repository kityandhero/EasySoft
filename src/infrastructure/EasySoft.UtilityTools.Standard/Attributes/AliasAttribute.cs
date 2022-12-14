namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// AliasAttribute
/// </summary>
public class AliasAttribute : Attribute
{
    /// <summary>
    /// Value
    /// </summary>
    public string Alias { get; }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="alias"></param>
    public AliasAttribute(string alias)
    {
        Alias = alias;
    }
}