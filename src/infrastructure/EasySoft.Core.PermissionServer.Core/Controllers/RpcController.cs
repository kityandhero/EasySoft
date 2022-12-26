using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Implements;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// rpc controller
/// </summary>
[Route("permissionRpc")]
public class RpcController : CustomControllerBase
{
    private readonly IPermissionRpcService _permissionRpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="permissionRpcService"></param>
    public RpcController(IPermissionRpcService permissionRpcService)
    {
        _permissionRpcService = permissionRpcService;
    }

    /// <summary>
    /// find access way   
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    [Route("findAccessWay")]
    [HttpPost]
    public async Task<RpcResult<AccessWayModel>> FindAccessWay(string guidTag)
    {
        var result = await _permissionRpcService.FindAccessWayAsync(guidTag);

        return result.ToRpcResult();
    }

    /// <summary>
    /// maintain preset super role   
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    [Route("maintainSuperRole")]
    [HttpPost]
    public async Task MaintainSuperRole(int channel)
    {
        await _permissionRpcService.MaintainSuper(channel);
    }
}