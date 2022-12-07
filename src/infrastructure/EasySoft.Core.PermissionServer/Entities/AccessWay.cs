using EasySoft.Core.PermissionServer.Entities.Bases;

namespace EasySoft.Core.PermissionServer.Entities;

/// <summary>
/// 访问模块
/// </summary>
[Description("访问模块")]
public class AccessWay : BaseEntity, IAccessWayPersistence
{
    [Description("名称")]
    public string Name { get; set; } = "";

    [Description("识别标识")]
    public string GuidTag { get; set; } = "";

    [Description("相对路径")]
    public string RelativePath { get; set; } = "";

    [Description("扩展权限")]
    public string Expand { get; set; } = "";

    [Description("渠道码")]
    public int Channel { get; set; }

    [Description("状态码")]
    public int Status { get; set; }

    [Description("Ip")]
    public string Ip { get; set; } = "";

    [Description("创建人标识")]
    public long CreateUserId { get; set; }

    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    [Description("更新人标识")]
    public long UpdateUserId { get; set; }

    [Description("更新时间")]
    public DateTime UpdateTime { get; set; }
}