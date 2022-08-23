using System.ComponentModel;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.Core.EasyToken.Attributes;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.EasyToken.Filters;

public abstract class OperatorCoreFilter : IOperatorAuthorizationFilter
{
    [Description("验证登录凭证")]
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var hasOperatorAttribute =
            filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            controllerActionDescriptor.MethodInfo.ContainAttribute<OperatorAttribute>();

        if (!hasOperatorAttribute)
        {
            return;
        }

        var token = filterContext.HttpContext.GetEasyToken(GeneralConfigAssist.GetEasyTokenName());

        if (string.IsNullOrWhiteSpace(token))
        {
            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "请登录后操作"
            );

            return;
        }

        AutofacAssist.Instance.Resolve<ITokenSecretOptions>();

        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        try
        {
            var identity = tokenSecret.DecryptWithExpirationTime(token, out var expired);

            if (expired)
            {
                filterContext.Result = new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "凭证无效或凭证已过期"
                );

                return;
            }

            var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

            actualOperator.SetToken(token);

            actualOperator.SetIdentity(identity);

            if (actualOperator.IsAnonymous())
            {
                filterContext.Result = new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "凭证无效或凭证已过期"
                );
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (!GeneralConfigAssist.GetRemoteErrorLogSwitch())
            {
                AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
                    e,
                    0,
                    filterContext.HttpContext.BuildRequestInfo()
                );
            }

            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "凭证不适配解析规则"
            );
        }
    }
}