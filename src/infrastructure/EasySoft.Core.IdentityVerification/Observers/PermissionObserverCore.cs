using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.Core.IdentityVerification.Tokens;
using EasySoft.UtilityTools.Competence;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.IdentityVerification.Observers;

public abstract class PermissionObserverCore : IPermissionObserver
{
    private readonly IOperator _operator;

    protected PermissionObserverCore(IOperator applicationOperator)
    {
        _operator = applicationOperator;
    }

    public IOperator GetOperator()
    {
        return _operator;
    }

    public abstract bool OnJudging();
    public abstract List<CompetenceEntity> GetCompetenceEntityCollection();

    public virtual bool CheckAccessPermission(string guidTag)
    {
        var listCompetenceEntity = GetCompetenceEntityCollection();

        foreach (var ce in listCompetenceEntity)
        {
            if (ce.GuidTag == guidTag.Remove("-") || ce.GuidTag == ConstCollection.SuperRoleGuidTag)
            {
                return true;
            }
        }

        return false;
    }
}