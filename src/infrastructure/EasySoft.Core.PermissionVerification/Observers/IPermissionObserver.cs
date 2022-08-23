using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.UtilityTools.Standard.Competence;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.PermissionVerification.Observers;

public interface IPermissionObserver
{
    public IActualOperator GetActualOperator();

    public bool OnJudging();

    public List<CompetenceEntity> GetCompetenceEntityCollection();

    public ExecutiveResult CheckAccessPermission(string guidTag);
}