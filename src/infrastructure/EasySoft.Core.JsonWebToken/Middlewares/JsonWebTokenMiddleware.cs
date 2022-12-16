using EasySoft.Core.JsonWebToken.Assists;
using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.JsonWebToken.Middlewares;

public class JsonWebTokenMiddleware : IMiddleware
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _hostEnvironment;

    public JsonWebTokenMiddleware(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        _loggerFactory = loggerFactory;
        _hostEnvironment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = _loggerFactory.CreateLogger<JsonWebTokenMiddleware>();

        logger.LogMiddlewareInvokeAsyncBefore<JsonWebTokenMiddleware>(_hostEnvironment);

        var operatorAttribute = context.TryGetMetadata<OperatorAttribute>();

        if (operatorAttribute == null)
        {
            await next.Invoke(context);

            return;
        }

        logger.LogMiddlewareInvokeAsyncAfter<JsonWebTokenMiddleware>(_hostEnvironment);

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
                AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
                    e,
                    0,
                    context.BuildRequestInfo()
                );

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
            await next.Invoke(context);
        else
            await context.Response.WriteObjectAsJsonAsync(
                new ApiResult(
                    ReturnCode.TokenExpired.ToInt(),
                    false,
                    "无操作凭证或凭证已过期"
                ).ToExpandoObject()
            );
    }
}