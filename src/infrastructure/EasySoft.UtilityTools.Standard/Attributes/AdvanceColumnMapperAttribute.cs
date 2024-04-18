namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// advance column
/// </summary>
public class AdvanceColumnMapperAttribute : DescriptionAttribute
{
    /// <summary>
    /// 列名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>  
    public AdvanceColumnMapperAttribute(string name, string description = "") : base(description)
    {
        Name = name;
    }
}