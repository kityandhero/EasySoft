using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.UtilityTools.AspNetCore.Extensions;
using EasySoft.UtilityTools.AspNetCore.Results.Implements;
using EasySoft.UtilityTools.Standard.Producers;

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

        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        var tokenNameConfigValue = authenticationConfigureAssist.GetTokenName();

        var token = filterContext.HttpContext.GetToken(tokenNameConfigValue.Value);

        if (string.IsNullOrWhiteSpace(token))
        {
            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "请登录后操作"
            );

            return;
        }

        ServiceProviderAssist.Resolve<ITokenSecretOptions>();

        var tokenSecret = ServiceProviderAssist.Resolve<ITokenSecret>();

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
                var applicationConfigureAssist = ServiceProviderAssist.Resolve<ApplicationConfigureAssist>();

                var transmissionErrorConfigValue = applicationConfigureAssist.GetUseErrorLogTransceiver();

                if (!transmissionErrorConfigValue.Value)
                {
                    ServiceProviderAssist.Resolve<IErrorLogProducer>()
                        .SendAsync(
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

            var actualOperator = ServiceProviderAssist.Resolve<IActualOperator>();

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
            var applicationConfigureAssist = ServiceProviderAssist.Resolve<ApplicationConfigureAssist>();

            var transmissionErrorConfigValue = applicationConfigureAssist.GetUseErrorLogTransceiver();

            if (!transmissionErrorConfigValue.Value)
            {
                ServiceProviderAssist.Resolve<IErrorLogProducer>()
                    .SendAsync(
                        e,
                        0,
                        filterContext.HttpContext.BuildRequestInfo()
                    );
            }

            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                HostEnvironmentAssist.GetEnvironment().IsDevelopment() ? e.Message : "校验凭证时发生错误",
                HostEnvironmentAssist.GetEnvironment().IsDevelopment()
                    ? new
                    {
                        stackTrace = e.StackTrace
                    }
                    : null
            );
        }
    }
}