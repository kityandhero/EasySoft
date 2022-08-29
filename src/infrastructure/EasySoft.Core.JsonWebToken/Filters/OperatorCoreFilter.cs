using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Filters;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasySoft.Core.JsonWebToken.Filters;

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

        actualOperator.SetIdentification(identification);

        if (actualOperator.IsAnonymous())
        {
            filterContext.Result = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                "凭证无效或凭证已过期"
            );
        }
    }
}