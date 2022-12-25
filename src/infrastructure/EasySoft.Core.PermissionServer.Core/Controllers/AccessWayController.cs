using EasySoft.Core.PermissionServer.Core.Controllers.Common;
using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Attributes;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// AccessWayController
/// </summary>
[Route("accessWay")]
public class AccessWayController : AuthControllerCore
{
    private const string ControllerDescription = "模块管理/";

    private readonly IAccessWayService _accessWayService;

    /// <summary>
    /// PresetRoleController
    /// </summary>
    /// <param name="accessWayService"></param>
    public AccessWayController(IAccessWayService accessWayService)
    {
        _accessWayService = accessWayService;
    }

    /// <summary>
    /// PageList
    /// </summary>
    /// <param name="presetRoleSearchDto"></param>
    /// <returns></returns>
    [Route("pageList")]
    [HttpPost]
    [Permission(ControllerDescription + "模块列表", "be7d114a-a189-4a5a-ae93-908dcd1ed3ca")]
    public async Task<IApiResult> PageList(AccessWaySearchDto presetRoleSearchDto)
    {
        var result = await _accessWayService.PageListAsync(presetRoleSearchDto);

        return this.PagedData(result, JsonConvertAssist.SerializeAndKeyToLower);
    }
}