using System.ComponentModel;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

/// <summary>
/// 角色组
/// </summary>
[Description("角色组")]
public class RoleGroup : BaseEntity
{
    [Description("自定义角色集合")]
    public string CustomRoleCollection { get; set; } = "";

    [Description("预设角色集合")]
    public string PresetRoleCollection { get; set; } = "";

    [Description("渠道码")]
    public int Channel { get; set; }

    public List<User>? Users { get; set; }
}