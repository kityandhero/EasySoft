namespace EasySoft.Core.PermissionServer.Core.DataTransferObjects;

/// <summary>
/// RoleGroupDto
/// </summary>
public class RoleGroupDto
{
    /// <summary>
    /// RoleGroupId
    /// </summary>
    public long RoleGroupId { get; set; }

    /// <summary>
    /// 角色组名
    /// </summary>
    [Description("角色组名")]
    public string Name { get; set; } = "";
}