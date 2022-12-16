using EasySoft.Core.PermissionServer.Core.Entities.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Entities.Bases;

/// <summary>
/// BaseRoleEntity
/// </summary>
public abstract class BaseRoleEntity : BaseEntity, IRoleEntity, IIp, IStatus, IOperate
{
    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string Description { get; set; } = "";

    /// <inheritdoc />
    public string Content { get; set; } = "";

    /// <inheritdoc />
    public int ModuleCount { get; set; } = 0;

    /// <inheritdoc />
    public string Competence { get; set; } = "";

    /// <inheritdoc />
    public int WhetherSuper { get; set; } = 0;

    /// <inheritdoc />
    public int Status { get; set; } = 0;

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; }
}