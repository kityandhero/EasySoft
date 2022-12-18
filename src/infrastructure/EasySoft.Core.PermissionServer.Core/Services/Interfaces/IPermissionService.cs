using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IPermissionService : IBusinessService
{
    /// <summary>
    /// get competence entity collection
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long userId);

    /// <summary>
    /// get competence entity collection
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    Task<IList<AccessWayModel>> FindAccessWayModelAsync(string guidTag);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="accessWayExchange"></param>
    /// <returns></returns>
    Task SaveAccessWayModelAsync(AccessWayExchange accessWayExchange);
}