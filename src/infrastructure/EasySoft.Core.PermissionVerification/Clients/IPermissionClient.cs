using EasySoft.Core.PermissionVerification.Entities;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EasySoft.Core.PermissionVerification.Clients;

/// <summary>
/// IPermissionClient
/// </summary>
public interface IPermissionClient
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    [Post("/accessWay/find")]
    public Task<ApiResponse<IList<AccessWayModel>>> FindAccessWayModel(string guidTag);

    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="roleGroupId"></param>
    /// <returns></returns>
    [Post("/competenceEntity/getCollection")]
    Task<ApiResponse<List<CompetenceEntity>>> GetCompetenceEntityCollectionAsync(long roleGroupId);
}