using EasySoft.Core.PermissionServer.Core.Controllers.Common;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// AccessWayController
/// </summary>
[Route("accessWay")]
public class AccessWayController : AuthControllerCore
{
    private readonly IRpcService _rpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="rpcService"></param>
    public AccessWayController(IRpcService rpcService)
    {
        _rpcService = rpcService;
    }
}