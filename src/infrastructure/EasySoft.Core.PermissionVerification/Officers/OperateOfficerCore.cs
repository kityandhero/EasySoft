using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.PermissionVerification.Officers;

public abstract class OperateOfficerCore : AccessWayOfficer
{
    private readonly IActualOperator _actualOperator;

    protected OperateOfficerCore()
    {
        _actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();
    }

    private IPermissionObserver? _permissionObserver;

    private IActualOperator GetOperator()
    {
        return _actualOperator;
    }

    protected IPermissionObserver GetPermissionObserver()
    {
        if (!AutofacAssist.Instance.IsRegistered<IPermissionObserver>())
        {
            throw new Exception("IPermissionObserver is not injected");
        }

        if (_permissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return _permissionObserver;
    }

    private void PrePareVerification()
    {
        _permissionObserver = AutofacAssist.Instance.Resolve<IPermissionObserver>();
    }

    /// <summary>
    /// 检测访问权限
    /// </summary>
    /// <returns></returns>
    private ExecutiveResult CheckAccessPermission()
    {
        var applicationOperator = GetOperator();

        if (applicationOperator.IsAnonymous())
        {
            return new ExecutiveResult(
                ReturnCode.NoPermission.ToMessage("匿名用户不支持鉴权, 请修复程序（配置登录验证）")
            );
        }

        if (string.IsNullOrWhiteSpace(AccessPermission.GuidTag))
        {
            return new ExecutiveResult(ReturnCode.Ok);
        }

        if (_permissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return _permissionObserver.CheckAccessPermission(AccessPermission.GuidTag);
    }

    protected ExecutiveResult<ApiResult> TryVerification()
    {
        CollectAccessWay();

        PrePareVerification();

        var result = CheckAccessPermission();

        if (result.Success)
        {
            return new ExecutiveResult<ApiResult>(ReturnCode.Ok);
        }

        var apiResultNoAccessPermission = new ApiResult(
            ReturnCode.NoPermission.ToInt(),
            false,
            result.Message
        );

        return new ExecutiveResult<ApiResult>(ReturnCode.AuthenticationFail)
        {
            Data = apiResultNoAccessPermission
        };
    }
}