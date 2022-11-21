namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// RegularAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class RegularAttribute : DescriptionAttribute
{
    /// <summary>
    /// RegularAttribute
    /// </summary>
    /// <param name="regular"></param>
    public RegularAttribute(string regular) : base(regular)
    {
    }
}