using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// CompetenceEntityController
/// </summary>
[Route("competenceEntity")]
public class CompetenceEntityController : CustomControllerBase
{
    private readonly IRpcService _rpcService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="rpcService"></param>
    public CompetenceEntityController(IRpcService rpcService)
    {
        _rpcService = rpcService;
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
        return await _rpcService.GetCompetenceEntityCollectionAsync(roleGroupId);
    }
}