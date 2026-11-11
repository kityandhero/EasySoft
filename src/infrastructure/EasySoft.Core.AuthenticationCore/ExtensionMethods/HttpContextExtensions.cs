namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// HttpContextExtensions
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// GetToken
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetToken(this HttpContext httpContext, string key = "token")
    {
        return httpContext.Request.GetToken();
    }
}