namespace EasySoft.Core.Sql.Attributes;

/// <summary>
/// advance column
/// </summary>
public class AdvanceColumnAttribute : Attribute
{
    /// <summary>
    /// 列名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    public AdvanceColumnAttribute(string name)
    {
        Name = name;
    }
}