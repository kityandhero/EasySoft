using EasySoft.Core.PermissionVerification.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.PermissionVerification.Remotes;

/// <summary>
/// IAccessWayApi
/// </summary>
public interface IAccessWayApi
{
    /// <summary>
    /// GetCompetenceEntityCollectionAsync
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    [HttpPost("/accessWay/find")]
    public Task<IList<AccessWayModel>> Find(string guidTag);
}