using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EasySoft.Core.JsonWebToken.Middlewares;

public class JsonWebTokenMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var operatorAttribute = context.TryGetAttribute<OperatorAttribute>();

        if (operatorAttribute == null)
        {
            await next.Invoke(context);

            return;
        }

        var token = context.GetToken(GeneralConfigAssist.GetTokenName());

        if (string.IsNullOrWhiteSpace(token))
        {
            await context.Response.WriteObjectAsJsonAsync(
                new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "无操作凭证或凭证已过期"
                ).ToExpandoObject()
            );

            return;
        }

        var first = context.User.Claims.FirstOrDefault(o => o.Type == JwtRegisteredClaimNames.Sub);

        if (first == null)
        {
            await context.Response.WriteObjectAsJsonAsync(
                new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "无操作凭证或凭证已过期"
                ).ToExpandoObject()
            );

            return;
        }

        var identification = first.Value;

        var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

        actualOperator.SetToken(token);

        actualOperator.SetIdentification(identification);

        if (!actualOperator.IsAnonymous())
        {
            await next.Invoke(context);
        }
        else
        {
            await context.Response.WriteObjectAsJsonAsync(
                new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "无操作凭证或凭证已过期"
                ).ToExpandoObject()
            );
        }
    }
}