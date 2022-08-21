using System.ComponentModel;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.IdentityVerification.AccessControl;
using EasySoft.Core.IdentityVerification.Attributes;
using EasySoft.Core.IdentityVerification.ExtensionMethods;
using EasySoft.Core.IdentityVerification.Observers;
using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Exceptions;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.IdentityVerification.Filters;

public abstract class OperatorCoreFilter : AccessWayFilter
{
    private IActualOperator? _actualOperator;

    private IPermissionObserver? _permissionObserver;

    private IActualOperator GetOperator()
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

        if (_permissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return _permissionObserver;
    }

    private bool HasOperator()
    {
        var applicationOperator = GetOperator();

        return applicationOperator.IsAnonymous();
    }

    /// <summary>
    /// 检测访问权限
    /// </summary>
    /// <param name="filterContext">上下文</param>
    /// <returns></returns>
    private bool CheckAccessPermission(AuthorizationFilterContext filterContext)
    {
        var applicationOperator = GetOperator();

        if (applicationOperator.IsAnonymous())
        {
            return false;
        }

        var guidTag = GetGuidTag(filterContext);

        if (string.IsNullOrWhiteSpace(guidTag))
        {
            return true;
        }

        if (_permissionObserver == null)
        {
            throw new Exception("PermissionObserver has not set");
        }

        return _permissionObserver.CheckAccessPermission(guidTag);
    }

    [Description("验证登录凭证以及操作权限")]
    public override void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var hasOperatorAttribute =
            filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            controllerActionDescriptor.MethodInfo.ContainAttribute<OperatorAttribute>();

        if (!hasOperatorAttribute)
        {
            return;
        }

        AutofacAssist.Instance.Resolve<ITokenSecretOptions>();
        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        var token = filterContext.HttpContext.GetToken();

        var identity = tokenSecret.DecryptWithExpirationTime(token, out var expired);

        if (expired)
        {
            throw new TokenException("登陆凭证已过期");
        }

        _actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

        _actualOperator.SetToken(token);

        _actualOperator.SetIdentity(identity);

        _permissionObserver = AutofacAssist.Instance.Resolve<IPermissionObserver>();

        CheckAccessWay(filterContext);

        var hasOperator = HasOperator();

        if (!hasOperator)
        {
            var result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "无操作凭证或凭证已过期"
            );

            filterContext.Result = new JsonResult(
                result,
                JsonConvertAssist.CreateJsonSerializerSettings()
            );

            return;
        }

        var checkAccessPermission = CheckAccessPermission(filterContext);

        if (checkAccessPermission)
        {
            return;
        }

        var apiResultNoAccessPermission = new ApiResult(
            ReturnCode.NoAccessPermission.ToInt(),
            false,
            "无访问权限"
        );

        filterContext.Result = new JsonResult(
            apiResultNoAccessPermission,
            JsonConvertAssist.CreateJsonSerializerSettings()
        );
    }
}