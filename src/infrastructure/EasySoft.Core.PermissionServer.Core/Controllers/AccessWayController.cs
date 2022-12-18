using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// AccessWayController
/// </summary>
[Route("accessWay")]
public class AccessWayController : CustomControllerBase
{
    private readonly IPermissionService _permissionService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="permissionService"></param>
    public AccessWayController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    [Route("find")]
    [HttpPost]
    public async Task<IList<AccessWayModel>> Find(string guidTag)
    {
        return await _permissionService.FindAccessWayModelAsync(guidTag);
    }
}