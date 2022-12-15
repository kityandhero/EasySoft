using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

[Route("accessWay")]
public class AccessWayController : CustomControllerBase
{
    private readonly ISecurityService _securityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="blogService"></param>
    public AccessWayController(ISecurityService blogService)
    {
        _securityService = blogService;
    }

    [Route("find")]
    [HttpPost]
    public async Task<IList<AccessWayModel>> Find(string guidTag)
    {
        return await _securityService.FindAccessWayModelAsync(guidTag);
    }
}