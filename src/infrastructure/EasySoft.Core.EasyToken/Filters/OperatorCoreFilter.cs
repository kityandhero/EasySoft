using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Implements;

namespace EasySoft.Core.EasyToken.Filters;

public abstract class OperatorCoreFilter : IOperatorAuthorizationFilter
{
    [Description("验证登录凭证")]
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var hasOperatorAttribute =
            filterContext.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            controllerActionDescriptor.MethodInfo.ContainAttribute<OperatorAttribute>();

        if (!hasOperatorAttribute) return;

        var token = filterContext.HttpContext.GetToken(GeneralConfigAssist.GetTokenName());

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
            string identity;
            bool expired;

            try
            {
                identity = tokenSecret.DecryptWithExpirationTime(token, out expired);
            }
            catch (Exception e)
            {
                if (!GeneralConfigAssist.GetRemoteErrorLogSwitch())
                    AutofacAssist.Instance.Resolve<IErrorLogProducer>().SendAsync(
                        e,
                        0,
                        filterContext.HttpContext.BuildRequestInfo()
                    );

                filterContext.Result = new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "凭证不适配解析规则"
                );

                return;
            }

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
                filterContext.Result = new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "凭证无效或凭证已过期"
                );
        }
        catch (Exception e)
        {
            if (!GeneralConfigAssist.GetRemoteErrorLogSwitch())
                AutofacAssist.Instance.Resolve<IErrorLogProducer>().SendAsync(
                    e,
                    0,
                    filterContext.HttpContext.BuildRequestInfo()
                );

            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                EnvironmentAssist.GetEnvironment().IsDevelopment() ? e.Message : "校验凭证时发生错误",
                EnvironmentAssist.GetEnvironment().IsDevelopment()
                    ? new
                    {
                        stackTrace = e.StackTrace
                    }
                    : null
            );
        }
    }
}