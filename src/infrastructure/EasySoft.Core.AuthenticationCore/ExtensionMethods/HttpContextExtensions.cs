using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class HttpContextExtensions
{
    public static string GetToken(this HttpContext c, string key = "token")
    {
        return c.Request.Headers.ContainsKey(key) ? c.Request.Headers[key] : "";
    }
}