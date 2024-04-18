namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// 字段长度
/// </summary>
public class AdvanceColumnLengthAttribute : DescriptionAttribute
{
    /// <summary>
    /// Length
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// AdvanceColumnLengthAttribute
    /// </summary>
    /// <param name="length"></param>
    public AdvanceColumnLengthAttribute(int length) : base("")
    {
        Length = length;
    }
}