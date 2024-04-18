namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// 数值精度
/// </summary>
public class AdvanceColumnAccuracyAttribute : DescriptionAttribute
{
    /// <summary>
    /// Accuracy
    /// </summary>
    public int Accuracy { get; }

    /// <summary>
    /// AdvanceColumnAccuracyAttribute
    /// </summary>
    /// <param name="accuracy"></param>
    public AdvanceColumnAccuracyAttribute(int accuracy) : base("")
    {
        Accuracy = accuracy;
    }
}