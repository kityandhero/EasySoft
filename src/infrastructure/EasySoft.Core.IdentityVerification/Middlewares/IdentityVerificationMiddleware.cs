using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.IdentityVerification.Officers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Core.ExtensionMethods;
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
            if (result.Data != null)
            {
                await context.Response.WriteObjectAsJsonAsync(result.Data.ToExpandoObject());
            }
        }
    }
}