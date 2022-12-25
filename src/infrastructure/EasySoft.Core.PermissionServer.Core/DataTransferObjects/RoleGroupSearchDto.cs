using EasySoft.UtilityTools.Standard.Params;

namespace EasySoft.Core.PermissionServer.Core.DataTransferObjects;

/// <summary>
/// RoleGroupSearchDto
/// </summary>
public class RoleGroupSearchDto : PageSearchParams
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