using EasySoft.Core.PermissionServer.Core.Entities.Bases;

namespace EasySoft.Core.PermissionServer.Core.Entities;

/// <summary>
/// 角色组
/// </summary>
[Description("角色组")]
public class RoleGroup : BaseEntity
{
    /// <summary>
    /// 角色组名
    /// </summary>
    [Description("角色组名")]
    public string Name { get; set; } = "";

    /// <summary>
    /// 自定义角色集合
    /// </summary>
    [Description("自定义角色集合")]
    public string CustomRoleCollection { get; set; } = "";

    /// <summary>
    /// 预设角色集合
    /// </summary>
    [Description("预设角色集合")]
    public string PresetRoleCollection { get; set; } = "";
}