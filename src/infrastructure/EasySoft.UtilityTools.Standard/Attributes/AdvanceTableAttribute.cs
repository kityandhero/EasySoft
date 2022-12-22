namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// advance table
/// </summary>
public class AdvanceTableAttribute : Attribute
{
    /// <summary>
    /// 表名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    public AdvanceTableAttribute(string name)
    {
        Name = name;
    }
}