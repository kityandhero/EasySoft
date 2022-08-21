using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.IdentityVerification.ExtensionMethods;

public static class HttpContextExtensions
{
    public static string GetToken(this HttpContext c)
    {
        return c.Request.Headers.ContainsKey("token") ? c.Request.Headers["token"] : "";
    }
}