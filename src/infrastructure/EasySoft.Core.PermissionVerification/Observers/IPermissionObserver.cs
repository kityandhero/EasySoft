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
    public List<CompetenceEntity> GetCompetenceEntityCollection();

    /// <summary>
    /// CheckAccessPermission
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public ExecutiveResult CheckAccessPermission(string guidTag);
}