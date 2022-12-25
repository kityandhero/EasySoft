using EasySoft.Core.PermissionServer.Core.Controllers.Common;
using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Attributes;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// PresetRoleController
/// </summary>
[Route("presetRole")]
public class PresetRoleController : AuthControllerCore
{
    private const string ControllerDescription = "预设角色管理/";

    private readonly IPresetRoleService _presetRoleService;

    /// <summary>
    /// PresetRoleController
    /// </summary>
    /// <param name="presetRoleService"></param>
    public PresetRoleController(IPresetRoleService presetRoleService)
    {
        _presetRoleService = presetRoleService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="presetRoleSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "角色列表", "c8830d0f-3140-466b-bea5-0d997d166d6d")]
    public async Task<IApiResult> PageList(PresetRoleSearchDto presetRoleSearchDto)
    {
        var result = await _presetRoleService.PageListAsync(presetRoleSearchDto);

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