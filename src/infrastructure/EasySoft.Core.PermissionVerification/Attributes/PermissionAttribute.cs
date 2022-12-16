namespace EasySoft.Core.PermissionVerification.Attributes;

/// <summary>
/// 权限配置
/// </summary>
public class PermissionAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// GuidTag
    /// </summary>
    public string GuidTag { get; }

    /// <summary>
    /// 权限组
    /// </summary>
    public string Group { get; }

    /// <summary>
    /// 配置项
    /// </summary>
    public ICollection<string> ExpandItems { get; private set; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    public PermissionAttribute() : this("", "")
    {
    }

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    public PermissionAttribute(string guidTag) : this("", guidTag)
    {
    }

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    /// <param name="name"></param>
    /// <param name="guidTag"></param>
    /// <param name="group"></param>
    public PermissionAttribute(string name, string guidTag, string group = "") : this(
        name,
        guidTag,
        group,
        Array.Empty<string>()
    )
    {
    }

    /// <summary>
    /// CompetenceConfigAttribute
    /// </summary>
    /// <param name="name"></param>
    /// <param name="guidTag"></param>
    /// <param name="group"></param>
    /// <param name="expandItems"></param>
    /// <exception cref="Exception"></exception>
    public PermissionAttribute(string name, string guidTag, string group, params string[] expandItems)
    {
        Name = name.Trim().Remove(" ");
        GuidTag = guidTag.Trim().Remove(" ");
        Group = group.Trim().Remove(" ");

        ExpandItems = new List<string>();

        foreach (var item in expandItems)
        {
            if (item.Contains('|')) throw new Exception("扩展权限中不能含有|");

            var itemAdjust = item.Trim().Remove(" ");

            if (!itemAdjust.IsTrimEmpty()) ExpandItems.Add(itemAdjust);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// 转换权限值为字符串形式
    /// </summary>
    /// <returns></returns>
    public string AggregateExpandItems()
    {
        var result = ExpandItems.Aggregate("", (current, c) => current + c + "|");

        result = result.TrimEnd('|');

        return result;
    }

    #endregion
}