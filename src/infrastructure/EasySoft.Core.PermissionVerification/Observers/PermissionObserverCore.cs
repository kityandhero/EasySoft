using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.UtilityTools.Standard.Competence;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.PermissionVerification.Observers;

public abstract class PermissionObserverCore : IPermissionObserver
{
    private readonly IActualOperator _actualOperator;

    protected PermissionObserverCore(IActualOperator applicationActualOperator)
    {
        _actualOperator = applicationActualOperator;
    }

    public IActualOperator GetActualOperator()
    {
        return _actualOperator;
    }

    public abstract bool OnJudging();
    public abstract List<CompetenceEntity> GetCompetenceEntityCollection();

    public virtual ExecutiveResult CheckAccessPermission(string guidTag)
    {
        if (string.IsNullOrWhiteSpace(guidTag))
        {
            return new ExecutiveResult(
                ReturnCode.NoPermission.ToMessage(
                    "接口未配置操作权限，如无需鉴权, 请移除 GuidTagAttribute 配置, 否则请修复配置值"
                )
            );
        }

        var listCompetenceEntity = GetCompetenceEntityCollection();

        foreach (var ce in listCompetenceEntity)
        {
            if (ce.GuidTag == guidTag.Remove("-") || ce.GuidTag == ConstCollection.SuperRoleGuidTag)
            {
                return new ExecutiveResult(ReturnCode.Ok);
            }
        }

        return new ExecutiveResult(ReturnCode.NoPermission.ToMessage("无权限操作"));
    }
}