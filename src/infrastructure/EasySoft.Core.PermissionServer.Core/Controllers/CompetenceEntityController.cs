using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// CompetenceEntityController
/// </summary>
[Route("competenceEntity")]
public class CompetenceEntityController : CustomControllerBase
{
    private readonly IPermissionRpcService _permissionRpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="permissionRpcService"></param>
    public CompetenceEntityController(IPermissionRpcService permissionRpcService)
    {
        _permissionRpcService = permissionRpcService;
    }

    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="roleGroupId"></param>
    /// <returns></returns>
    [Route("getCollection")]
    [HttpPost]
    public async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long roleGroupId)
    {
        return await _permissionRpcService.GetCompetenceEntityCollectionAsync(roleGroupId);
    }
}