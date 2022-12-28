using EasySoft.UtilityTools.Core.Results.Implements;

namespace EasySoft.Core.JsonWebToken.Filters;

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

        var first = filterContext.HttpContext.User.Claims
            .FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Sub);

        if (first == null)
        {
            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "凭证无效或凭证已过期"
            );

            return;
        }

        var identification = first.Value;

        var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

        actualOperator.SetToken(token);

        actualOperator.SetIdentity(identification);

        if (actualOperator.IsAnonymous())
            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "凭证无效或凭证已过期"
            );
    }
}