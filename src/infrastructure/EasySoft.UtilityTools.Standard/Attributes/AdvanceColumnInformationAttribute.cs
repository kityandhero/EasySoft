namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// 列信息
/// </summary>
public class AdvanceColumnInformationAttribute : DescriptionAttribute
{
    /// <summary>
    /// Label
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// AdvanceColumnInformationAttribute
    /// </summary>
    public AdvanceColumnInformationAttribute() : this("")
    {
    }

    /// <summary>
    /// AdvanceColumnInformationAttribute
    /// </summary>
    /// <param name="label"></param>
    /// <param name="description"></param>
    public AdvanceColumnInformationAttribute(string label, string description = "") : base(description)
    {
        Label = label;
    }
}