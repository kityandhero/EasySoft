using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class HttpResponseExtensions
{
    public static void SetCookie(this HttpResponse response, string key, string value)
    {
        response.SetCookie(key, value, new CookieOptions());
    }

    public static void SetCookie(this HttpResponse response, string key, string value, CookieOptions options)
    {
        response.Cookies.Append(key, value, options);
    }
}