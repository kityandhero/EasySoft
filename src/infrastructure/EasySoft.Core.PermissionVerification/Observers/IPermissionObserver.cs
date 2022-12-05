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
    public IActualOperator GetActualOperator();

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    public bool OnJudging();

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    public Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync();

    /// <summary>
    /// CheckAccessPermission
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public Task<ExecutiveResult> CheckAccessPermissionAsync(string guidTag);
}