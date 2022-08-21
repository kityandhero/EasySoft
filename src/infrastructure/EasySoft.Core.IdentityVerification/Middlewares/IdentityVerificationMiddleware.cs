using System.Text.Json;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.IdentityVerification.Officers;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Exceptions;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.IdentityVerification.Middlewares;

public class IdentityVerificationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var operateOfficer = AutofacAssist.Instance.Resolve<IOperateOfficer>();

        var result = operateOfficer.DoAuthorization(context);

        if (result.Success)
        {
            await next.Invoke(context);
        }
        else
        {
            await context.Response.WriteAsNewtonsoftJsonAsync(result.Data);
        }
    }
}