using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.EasyToken.Middlewares;

public class EasyTokenMiddleware : IMiddleware
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EasyTokenMiddleware(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        _loggerFactory = loggerFactory;
        _hostEnvironment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = _loggerFactory.CreateLogger<EasyTokenMiddleware>();

        logger.LogMiddlewareInvokeAsyncBefore<EasyTokenMiddleware>(_hostEnvironment);

        var operatorAttribute = context.TryGetMetadata<OperatorAttribute>();

        if (operatorAttribute == null)
        {
            await next.Invoke(context);

            return;
        }

        logger.LogMiddlewareInvokeAsyncAfter<EasyTokenMiddleware>(_hostEnvironment);

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
                        context.BuildRequestInfo()
                    );

                await context.Response.WriteObjectAsJsonAsync(
                    new ApiResult(
                        ReturnCode.TokenExpired.ToInt(),
                        false,
                        "凭证不适配解析规则"
                    ).ToExpandoObject()
                );

                return;
            }

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
        catch (Exception e)
        {
            if (!GeneralConfigAssist.GetRemoteErrorLogSwitch())
                AutofacAssist.Instance.Resolve<IErrorLogProducer>().SendAsync(
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
        }
    }
}