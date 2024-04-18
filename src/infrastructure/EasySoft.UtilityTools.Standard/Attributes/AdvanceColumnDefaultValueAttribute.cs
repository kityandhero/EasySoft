namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// 字段默认值
/// </summary>
public class AdvanceColumnDefaultValueAttribute : DescriptionAttribute
{
    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// AdvanceColumnDefaultValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public AdvanceColumnDefaultValueAttribute(string value) : base("")
    {
        Value = value;
    }
}