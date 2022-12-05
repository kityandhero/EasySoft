using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Officers;

namespace EasySoft.Core.PermissionVerification.Middlewares;

/// <summary>
/// PermissionVerificationMiddleware
/// </summary>
public class PermissionVerificationMiddleware : IMiddleware
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IWebHostEnvironment _hostEnvironment;

    /// <summary>
    /// PermissionVerificationMiddleware
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    public PermissionVerificationMiddleware(ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        _loggerFactory = loggerFactory;
        _hostEnvironment = environment;
    }

    /// <summary>
    /// InvokeAsync
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
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

        var result = await operateOfficer.DoVerificationAsync(context);

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