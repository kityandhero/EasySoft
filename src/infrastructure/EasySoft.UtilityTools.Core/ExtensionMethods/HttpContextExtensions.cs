using System.IO;
using EasySoft.UtilityTools.Standard.Entity;
using Microsoft.AspNetCore.Http;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class HttpContextExtensions
{
    /// <summary>
    /// 当前请求的客户端IP
    /// </summary>
    public static string GetCurrentAddress(this HttpContext httpContext)
    {
        var ip = httpContext.Request.Headers["Cdn-Src-Ip"].FirstOrDefault();

        if (!string.IsNullOrEmpty(ip)) return IpReplace(ip);

        ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrEmpty(ip)) return IpReplace(ip);

        ip = httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "";

        if (ip == "::1") ip = "127.0.0.1";

        return ip;
    }

    public static string GetClientIP(this HttpContext context)
    {
        var ip = context.Request.Headers["Cdn-Src-Ip"].FirstOrDefault();
        if (!string.IsNullOrEmpty(ip))
            return IpReplace(ip);

        ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(ip))
            return IpReplace(ip);

        ip = context.Connection.RemoteIpAddress?.ToString() ?? "";

        return IpReplace(ip);
    }

    private static string IpReplace(string ip)
    {
        //::ffff:
        //::ffff:192.168.2.131 这种IP处理
        if (ip.Contains("::ffff:")) ip = ip.Replace("::ffff:", "");

        return ip;
    }

    public static IFormFileCollection GetFiles(this HttpContext context)
    {
        return context.Request.GetFiles();
    }

    public static IFormFileCollection GetFiles(this HttpRequest request)
    {
        return request.HasFormContentType ? request.Form.Files : new FormFileCollection();
    }

    public static void SaveAs(this IFormFile file, string path)
    {
        using (var fs = new FileStream(path, FileMode.CreateNew))
        {
            file.CopyTo(fs);

            fs.Flush();
        }
    }

    public static T? TryGetAttribute<T>(this HttpContext context) where T : class
    {
        var endpoint = context.GetEndpoint();

        // Endpoint will be null if the URL didn't match an action, e.g. a 404 response
        return endpoint?.Metadata.GetMetadata<T>();
    }

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