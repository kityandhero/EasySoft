namespace EasySoft.Core.PermissionVerification.Observers;

/// <summary>
/// PermissionObserverCore
/// </summary>
public abstract class PermissionObserverCore : IPermissionObserver
{
    private readonly IActualOperator _actualOperator;

    /// <summary>
    /// PermissionObserverCore
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    protected PermissionObserverCore(IActualOperator applicationActualOperator)
    {
        _actualOperator = applicationActualOperator;
    }

    /// <summary>
    /// GetActualOperator
    /// </summary>
    /// <returns></returns>
    public IActualOperator GetActualOperator()
    {
        return _actualOperator;
    }

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    public abstract bool OnJudging();

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    public abstract List<CompetenceEntity> GetCompetenceEntityCollection();

    /// <summary>
    /// CheckAccessPermission
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public virtual ExecutiveResult CheckAccessPermission(string guidTag)
    {
        if (string.IsNullOrWhiteSpace(guidTag))
            return new ExecutiveResult(
                ReturnCode.NoPermission.ToMessage(
                    "接口未配置操作权限，如无需鉴权, 请移除 GuidTagAttribute 配置, 否则请修复配置值"
                )
            );

        var listCompetenceEntity = GetCompetenceEntityCollection();

        foreach (var ce in listCompetenceEntity)
            if (ce.GuidTag == guidTag.Remove("-") || ce.GuidTag == ConstCollection.SuperRoleGuidTag)
                return new ExecutiveResult(ReturnCode.Ok);

        return new ExecutiveResult(ReturnCode.NoPermission.ToMessage("无权限操作"));
    }
}