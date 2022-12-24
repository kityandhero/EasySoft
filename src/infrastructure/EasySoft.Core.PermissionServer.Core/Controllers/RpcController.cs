using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Implements;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// rpc controller
/// </summary>
[Route("rpc")]
public class RpcController : CustomControllerBase
{
    private readonly IRpcService _rpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="rpcService"></param>
    public RpcController(IRpcService rpcService)
    {
        _rpcService = rpcService;
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
        var result = await _rpcService.FindAccessWayAsync(guidTag);

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
        await _rpcService.MaintainSuperRole(channel);
    }
}