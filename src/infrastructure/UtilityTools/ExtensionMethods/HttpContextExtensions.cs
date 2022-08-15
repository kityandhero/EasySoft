using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace UtilityTools.ExtensionMethods;

public static class HttpContextExtensions
{
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

    static string IpReplace(string ip)
    {
        //::ffff:
        //::ffff:192.168.2.131 这种IP处理
        if (ip.Contains("::ffff:"))
        {
            ip = ip.Replace("::ffff:", "");
        }

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
}