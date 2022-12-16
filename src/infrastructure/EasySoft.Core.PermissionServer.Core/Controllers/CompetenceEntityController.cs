﻿using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Controllers;

[Route("competenceEntity")]
public class CompetenceEntityController : CustomControllerBase
{
    private readonly ISecurityService _securityService;

    /// <summary>
    /// EntranceController
    /// </summary>
    /// <param name="blogService"></param>
    public CompetenceEntityController(ISecurityService blogService)
    {
        _securityService = blogService;
    }

    [Route("getCollection")]
    [HttpPost]
    public async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long roleGroupId)
    {
        return await _securityService.GetCompetenceEntityCollectionAsync(roleGroupId);
    }
}