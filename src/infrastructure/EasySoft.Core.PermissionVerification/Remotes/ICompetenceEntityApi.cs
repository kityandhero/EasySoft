using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.PermissionVerification.Remotes;

/// <summary>
/// ICompetenceEntityApi
/// </summary>
public interface ICompetenceEntityApi
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="roleGroupId"></param>
    /// <returns></returns>
    [HttpPost("/competenceEntity/getCollection")]
    Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long roleGroupId);
}