﻿using System.Security.Claims;
using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.AuthenticationCore.Tools;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.JsonWebToken.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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

        var token = await context.GetTokenAsync(GeneralConfigAssist.GetTokenName());

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

        ClaimsPrincipal claimsPrincipal;

        try
        {
            claimsPrincipal = TokenAssist.ValidateToken(token);
        }
        catch (Exception e)
        {
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
                    EnvironmentAssist.GetEnvironment().IsDevelopment() ? e.Message : "校验凭证时发生错误",
                    EnvironmentAssist.GetEnvironment().IsDevelopment()
                        ? new
                        {
                            stackTrace = e.StackTrace
                        }
                        : null
                ).ToExpandoObject()
            );

            return;
        }

        var first = claimsPrincipal.Claims.FirstOrDefault(o =>
            o.Type == JwtRegisteredClaimSpecialNames.EasySoftTokenIdentity
        );

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