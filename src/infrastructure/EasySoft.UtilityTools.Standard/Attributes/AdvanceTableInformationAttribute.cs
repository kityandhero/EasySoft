namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// AdvanceTableInformationAttribute
/// </summary>
public class AdvanceTableInformationAttribute : DescriptionAttribute
{
    /// <summary>
    /// Label
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// AdvanceTableInformationAttribute
    /// </summary>
    public AdvanceTableInformationAttribute() : this("")
    {
    }

    /// <summary>
    /// AdvanceTableInformationAttribute
    /// </summary>
    /// <param name="label"></param>
    /// <param name="description"></param>
    public AdvanceTableInformationAttribute(string label, string description = "") : base(description)
    {
        Label = label;
    }
}