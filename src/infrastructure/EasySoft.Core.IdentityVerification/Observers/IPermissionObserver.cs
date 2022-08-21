using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.UtilityTools.Competence;

namespace EasySoft.Core.IdentityVerification.Observers;

public interface IPermissionObserver
{
    public IActualOperator GetActualOperator();

    public bool OnJudging();

    public List<CompetenceEntity> GetCompetenceEntityCollection();

    public bool CheckAccessPermission(string guidTag);
}