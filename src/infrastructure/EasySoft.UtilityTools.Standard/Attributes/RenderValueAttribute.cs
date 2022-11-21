namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// RenderValueAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class RenderValueAttribute : DescriptionAttribute
{
    /// <summary>
    /// RenderValueAttribute
    /// </summary>
    /// <param name="renderValue"></param>
    public RenderValueAttribute(string renderValue) : base(renderValue)
    {
    }

    /// <summary>
    /// RenderValueAttribute
    /// </summary>
    public RenderValueAttribute() : base("")
    {
    }
}