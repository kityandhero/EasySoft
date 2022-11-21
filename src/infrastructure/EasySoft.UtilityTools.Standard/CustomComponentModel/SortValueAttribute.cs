namespace EasySoft.UtilityTools.Standard.CustomComponentModel;

/// <summary>
/// 定义排序序列
/// </summary>
public class SortValueAttribute : DescriptionAttribute
{
    public int Sort { get; }

    /// <summary>
    /// SortValueAttribute
    /// </summary>
    public SortValueAttribute() : this(0)
    {
    }

    /// <summary>
    /// SortValueAttribute
    /// </summary>
    /// <param name="sort"></param>
    public SortValueAttribute(int sort) : base(sort.ToString())
    {
        Sort = sort;
    }
}