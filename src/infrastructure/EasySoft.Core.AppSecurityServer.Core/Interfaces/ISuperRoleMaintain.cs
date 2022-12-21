namespace EasySoft.Core.AppSecurityServer.Core.Interfaces;

/// <summary>
/// 超级角色维护时间
/// </summary>
public interface ISuperRoleMaintain
{
    /// <summary>
    /// 超级角色最近维护时间
    /// </summary>
    [Description("SuperRoleMaintainTime")]
    public DateTime SuperRoleRecentlyMaintainTime { get; set; }

    /// <summary>
    /// 超级角色下次维护时间
    /// </summary>
    [Description("SuperRoleMaintainTime")]
    public DateTime SuperRoleNextMaintainTime { get; set; }
}