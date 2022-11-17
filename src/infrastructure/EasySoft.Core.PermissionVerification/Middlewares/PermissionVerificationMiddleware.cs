using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;

namespace EasySoft.Core.PermissionVerification.Middlewares;

public class PermissionVerificationMiddleware : IMiddleware
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _hostEnvironment;

    public PermissionVerificationMiddleware(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        _loggerFactory = loggerFactory;
        _hostEnvironment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = _loggerFactory.CreateLogger<PermissionVerificationMiddleware>();

        logger.LogMiddlewareInvokeAsyncBefore<PermissionVerificationMiddleware>(_hostEnvironment);

        var guidTagAttribute = context.TryGetMetadata<GuidTagAttribute>();

        if (guidTagAttribute == null)
        {
            await next.Invoke(context);

            return;
        }

        logger.LogMiddlewareInvokeAsyncAfter<PermissionVerificationMiddleware>(_hostEnvironment);

        var operateOfficer = AutofacAssist.Instance.Resolve<IOperateOfficer>();

        var result = operateOfficer.DoVerification(context);

        if (result.Success)
        {
            await next.Invoke(context);
        }
        else
        {
            if (result.Data != null) await context.Response.WriteObjectAsJsonAsync(result.Data.ToExpandoObject());
        }
    }
}