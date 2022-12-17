using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// CompetenceEntityController
/// </summary>
[Route("competenceEntity")]
public class CompetenceEntityController : CustomControllerBase
{
    private readonly IPermissionService _permissionService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="permissionService"></param>
    public CompetenceEntityController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
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
        return await _permissionService.GetCompetenceEntityCollectionAsync(roleGroupId);
    }
}