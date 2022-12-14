namespace EasySoft.Core.PermissionServer.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface ISecurityService : IBusinessService
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