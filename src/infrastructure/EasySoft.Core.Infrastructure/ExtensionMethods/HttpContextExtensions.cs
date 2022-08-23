using EasySoft.UtilityTools.Standard.Entity;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class HttpContextExtensions
{
    public static RequestInfo BuildRequestInfo(this HttpContext httpContext)
    {
        return httpContext.Request.BuildRequestInfo();
    }

    public static string GetCookie(this HttpContext httpContext, string key)
    {
        return httpContext.Request.Cookies[key] ?? "";
    }

    public static void SetCookie(this HttpContext httpContext, string key, string value)
    {
        httpContext.SetCookie(key, value, new CookieOptions());
    }

    public static void SetCookie(this HttpContext httpContext, string key, string value, CookieOptions options)
    {
        httpContext.Response.Cookies.Append(key, value, options);
    }
}