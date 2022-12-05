namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// IAccessWayPersistence
/// </summary>
public interface IAccessWayPersistence
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
    /// 
    /// </summary>
    [Description("相对路径")]
    public string RelativePath { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Description("扩展权限")]
    public string Expand { get; set; }
}