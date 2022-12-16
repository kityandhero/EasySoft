namespace EasySoft.Core.PermissionVerification.Entities;

/// <summary>
/// AccessWayModel
/// </summary>
public class AccessWayModel
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
    /// 扩展权限
    /// </summary>
    [Description("扩展权限")]
    public string Expand { get; set; }

    /// <summary>
    /// 组标识
    /// </summary>
    [Description("组标识")]
    public string Group { get; set; }

    /// <summary>
    /// 渠道码
    /// </summary>
    [Description("渠道码")]
    public int Channel { get; set; }

    /// <summary>
    /// AccessWayModel
    /// </summary>
    public AccessWayModel()
    {
        Name = "";
        GuidTag = "";
        RelativePath = "";
        Expand = "";
        Group = "";
        Channel = 0;
    }
}