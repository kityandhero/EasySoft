namespace EasySoft.UtilityTools.Standard.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class RenderValueAttribute : DescriptionAttribute
{
    public RenderValueAttribute(string renderValue) : base(renderValue)
    {
    }

    public RenderValueAttribute() : base("")
    {
    }
}