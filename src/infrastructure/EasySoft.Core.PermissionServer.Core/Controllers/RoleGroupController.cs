using EasySoft.Core.PermissionServer.Core.Controllers.Common;
using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Attributes;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// RoleGroupController
/// </summary>
[Route("roleGroup")]
public class RoleGroupController : AuthControllerCore
{
    private const string ControllerDescription = "角色组管理/";

    private readonly IRoleGroupService _roleGroupService;

    /// <summary>
    /// PresetRoleController
    /// </summary>
    /// <param name="roleGroupService"></param>
    public RoleGroupController(IRoleGroupService roleGroupService)
    {
        _roleGroupService = roleGroupService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="roleGroupSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "角色组列表", "027af027-cbcc-48b6-a853-844fd240d40d")]
    public async Task<IApiResult> PageList(RoleGroupSearchDto roleGroupSearchDto)
    {
        var result = await _roleGroupService.PageListAsync(roleGroupSearchDto);

        return this.Success(
            result.List,
            new
            {
                pageNo = result.PageIndex,
                pageSize = result.PageSize,
                total = result.TotalSize
            }
        );
    }
}