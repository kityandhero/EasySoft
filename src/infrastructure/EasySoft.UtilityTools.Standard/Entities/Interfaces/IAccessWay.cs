namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// IAccessWayPersistence
/// </summary>
public interface IAccessWay
{
    /// <summary>
    /// 名称
    /// </summary>
    [Description("名称")]
    string Name { get; set; }

    /// <summary>
    /// 识别标识
    /// </summary>
    [Description("识别标识")]
    string GuidTag { get; set; }

    /// <summary>
    /// 相对路径
    /// </summary>
    [Description("相对路径")]
    string RelativePath { get; set; }

    /// <summary>
    /// 扩展权限
    /// </summary>
    [Description("扩展权限")]
    string Expand { get; set; }

    /// <summary>
    /// 分组标识
    /// </summary>
    [Description("分组标识")]
    string Group { get; set; }

    /// <summary>
    /// 渠道码
    /// </summary>
    [Description("渠道码")]
    int Channel { get; set; }
}