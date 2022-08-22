using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.IdentityVerification.AccessControl;
using EasySoft.Core.IdentityVerification.Observers;
using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Exceptions;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.IdentityVerification.Officers;

public class OperateOfficerCore : AccessWayOfficer
{
    private IActualOperator? _actualOperator;

    protected IPermissionObserver? PermissionObserver;

    protected IActualOperator GetOperator()
    {
        if (!AutofacAssist.Instance.IsRegistered<IActualOperator>())
        {
            throw new Exception("IOperator is not injected");
        }

        if (_actualOperator == null)
        {
            throw new Exception("Operator has not set");
        }

        return _actualOperator;
    }

    protected IPermissionObserver GetPermissionObserver()
    {
        if (!AutofacAssist.Instance.IsRegistered<IPermissionObserver>())
        {
            throw new Exception("IPermissionObserver is not injected");
        }

        if (PermissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return PermissionObserver;
    }

    protected bool HasOperator()
    {
        var applicationOperator = GetOperator();

        return applicationOperator.IsAnonymous();
    }

    protected void PrePareAuthorization(string token)
    {
        AutofacAssist.Instance.Resolve<ITokenSecretOptions>();

        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        var identity = tokenSecret.DecryptWithExpirationTime(token, out var expired);

        if (expired)
        {
            throw new TokenException("登陆凭证已过期");
        }

        _actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

        _actualOperator.SetToken(token);

        _actualOperator.SetIdentity(identity);

        PermissionObserver = AutofacAssist.Instance.Resolve<IPermissionObserver>();
    }

    /// <summary>
    /// 检测访问权限
    /// </summary>
    /// <returns></returns>
    protected bool CheckAccessPermission()
    {
        var applicationOperator = GetOperator();

        if (applicationOperator.IsAnonymous())
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(AccessPermission.GuidTag))
        {
            return true;
        }

        if (PermissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return PermissionObserver.CheckAccessPermission(AccessPermission.GuidTag);
    }

    protected ExecutiveResult<ApiResult> TryAuthorization(string token)
    {
        CollectAccessWay();

        PrePareAuthorization(token);

        var hasOperator = HasOperator();

        if (!hasOperator)
        {
            var result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "无操作凭证或凭证已过期"
            );

            return new ExecutiveResult<ApiResult>(ReturnCode.AuthenticationFail)
            {
                Data = result
            };
        }

        var checkAccessPermission = CheckAccessPermission();

        if (checkAccessPermission)
        {
            return new ExecutiveResult<ApiResult>(ReturnCode.Ok);
        }

        var apiResultNoAccessPermission = new ApiResult(
            ReturnCode.NoAccessPermission.ToInt(),
            false,
            "无访问权限"
        );

        return new ExecutiveResult<ApiResult>(ReturnCode.AuthenticationFail)
        {
            Data = apiResultNoAccessPermission
        };
    }
}