using EasySoft.UtilityTools.Standard.Entity;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// HttpContextExtensions
/// </summary>
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

    /// <summary>
    /// GetClientIP
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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

    /// <summary>
    /// GetFiles
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IFormFileCollection GetFiles(this HttpContext context)
    {
        return context.Request.GetFiles();
    }

    /// <summary>
    /// GetFiles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static IFormFileCollection GetFiles(this HttpRequest request)
    {
        return request.HasFormContentType ? request.Form.Files : new FormFileCollection();
    }

    /// <summary>
    /// SaveAs
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    public static void SaveAs(this IFormFile file, string path)
    {
        using (var fs = new FileStream(path, FileMode.CreateNew))
        {
            file.CopyTo(fs);

            fs.Flush();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? TryGetMetadata<T>(this HttpContext context) where T : class
    {
        var endpoint = context.GetEndpoint();

        // Endpoint will be null if the URL didn't match an action, e.g. a 404 response
        return endpoint?.Metadata.GetMetadata<T>();
    }

    /// <summary>
    /// BuildRequestInfo
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static RequestInfo BuildRequestInfo(this HttpContext httpContext)
    {
        return httpContext.Request.BuildRequestInfo();
    }

    /// <summary>
    /// GetCookie
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetCookie(this HttpContext httpContext, string key)
    {
        return httpContext.Request.Cookies[key] ?? "";
    }

    /// <summary>
    /// SetCookie
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetCookie(this HttpContext httpContext, string key, string value)
    {
        httpContext.SetCookie(key, value, new CookieOptions());
    }

    /// <summary>
    /// SetCookie
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public static void SetCookie(this HttpContext httpContext, string key, string value, CookieOptions options)
    {
        httpContext.Response.Cookies.Append(key, value, options);
    }
}