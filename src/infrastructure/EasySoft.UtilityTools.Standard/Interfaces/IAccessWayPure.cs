namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IAccessWayPure
/// </summary>
public interface IAccessWayPure : ITriggerChannel
{
    /// <summary>
    /// 名称 
    /// </summary>
    [Description("名称")]
    public string Name { get; set; }

    /// <summary>
    /// 识别标识
    /// </summary>
    [Description("识别标识")]
    public string GuidTag { get; set; }

    /// <summary>
    /// 相对路径
    /// </summary>
    [Description("相对路径")]
    public string RelativePath { get; set; }

    /// <summary>
    /// 路径等级
    /// </summary>
    [Description("相对路径等级")]
    public int RelativePathLevel { get; set; }

    /// <summary>
    /// 父级路径
    /// </summary>
    [Description("相对父级路径")]
    public string RelativeParentPath { get; set; }

    /// <summary>
    /// 父级路径等级
    /// </summary>
    [Description("相对父级路径等级")]
    public int RelativeParentPathLevel { get; set; }

    /// <summary>
    /// 扩展权限
    /// </summary>
    [Description("扩展")]
    public string Expand { get; set; }

    /// <summary>
    /// 结果类型
    /// </summary>
    [Description("结果类型")]
    public string ResultType { get; set; }

    /// <summary>
    /// 分组
    /// </summary>
    [Description("分组")]
    string Group { get; set; }
}