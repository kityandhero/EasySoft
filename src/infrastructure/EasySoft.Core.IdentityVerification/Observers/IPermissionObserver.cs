using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.Core.IdentityVerification.Tokens;
using EasySoft.UtilityTools.Competence;

namespace EasySoft.Core.IdentityVerification.Observers;

public interface IPermissionObserver
{
    public IOperator GetOperator();

    public bool OnJudging();

    public List<CompetenceEntity> GetCompetenceEntityCollection();

    public bool CheckAccessPermission(string guidTag);
}