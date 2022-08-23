using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.EasyToken.Middlewares;

public class EasyTokenMiddleware : IMiddleware
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

        AutofacAssist.Instance.Resolve<ITokenSecretOptions>();

        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        try
        {
            var identity = tokenSecret.DecryptWithExpirationTime(token, out var expired);

            if (expired)
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

            var actualOperator = AutofacAssist.Instance.Resolve<IActualOperator>();

            actualOperator.SetToken(token);

            actualOperator.SetIdentification(identity);

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
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (!GeneralConfigAssist.GetRemoteErrorLogSwitch())
            {
                AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
                    e,
                    0,
                    context.BuildRequestInfo()
                );
            }

            await context.Response.WriteObjectAsJsonAsync(
                new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "凭证不适配解析规则"
                ).ToExpandoObject()
            );
        }
    }
}