using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

/// <summary>
/// CompetenceEntityController
/// </summary>
[Route("competenceEntity")]
public class CompetenceEntityController : CustomControllerBase
{
    private readonly ISecurityService _securityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="securityService"></param>
    public CompetenceEntityController(ISecurityService securityService)
    {
        _securityService = securityService;
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
        return await _securityService.GetCompetenceEntityCollectionAsync(roleGroupId);
    }
}