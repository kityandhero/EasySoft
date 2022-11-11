using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;

namespace EasySoft.Core.PermissionVerification.Middlewares;

public class PermissionVerificationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var guidTagAttribute = context.TryGetAttribute<GuidTagAttribute>();

        if (guidTagAttribute == null)
        {
            await next.Invoke(context);

            return;
        }

        var operateOfficer = AutofacAssist.Instance.Resolve<IOperateOfficer>();

        var result = operateOfficer.DoVerification(context);

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