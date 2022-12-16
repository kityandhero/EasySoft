using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// AccessWayController
/// </summary>
[Route("accessWay")]
public class AccessWayController : CustomControllerBase
{
    private readonly ISecurityService _securityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="securityService"></param>
    public AccessWayController(ISecurityService securityService)
    {
        _securityService = securityService;
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
        return await _securityService.FindAccessWayModelAsync(guidTag);
    }
}