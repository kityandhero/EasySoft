using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class HttpContextExtensions
{
    public static string GetToken(this HttpContext httpContext, string key = "token")
    {
        return httpContext.Request.GetToken();
    }

    public static async Task<string> GetTokenAsync(this HttpContext httpContext, string key = "token")
    {
        return await httpContext.Request.GetTokenAsync();
    }
}