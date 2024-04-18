using EasySoft.Core.PermissionServer.Core.Entities.Bases;

namespace EasySoft.Core.PermissionServer.Core.Entities;

/// <summary>
/// 访问模块
/// </summary>
[Description("访问模块")]
public class AccessWay : BaseEntity, IAccessWay, IIp, IStatus, IOperate
{
    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string GuidTag { get; set; } = "";

    /// <inheritdoc />
    public string RelativePath { get; set; } = "";

    /// <inheritdoc />
    public int RelativePathLevel { get; set; }

    /// <inheritdoc />
    public string RelativeParentPath { get; set; } = "";

    /// <inheritdoc />
    public int RelativeParentPathLevel { get; set; }

    /// <inheritdoc />
    public string Expand { get; set; } = "";

    /// <inheritdoc />
    public string ResultType { get; set; } = "";

    /// <inheritdoc />
    public string Group { get; set; } = "";

    /// <inheritdoc />
    public int Channel { get; set; }

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public int TriggerChannel { get; set; }
}