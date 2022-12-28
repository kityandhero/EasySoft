using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.PermissionVerification.Observers;

/// <summary>
/// IPermissionObserver
/// </summary>
public interface IPermissionObserver
{
    /// <summary>
    /// GetActualOperator
    /// </summary>
    /// <returns></returns>
    IActualOperator GetActualOperator();

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    bool OnJudging();

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    Task<IList<CompetenceEntity>> GetCompetenceEntityCollectionAsync(string identity);

    /// <summary>
    /// CheckAccessPermission
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    Task<ExecutiveResult> CheckAccessPermissionAsync(string guidTag);
}