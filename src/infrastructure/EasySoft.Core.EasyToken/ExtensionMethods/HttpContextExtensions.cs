using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class HttpContextExtensions
{
    public static string GetEasyToken(this HttpContext c, string key = "token")
    {
        return c.Request.Headers.ContainsKey(key) ? c.Request.Headers[key] : "";
    }
}