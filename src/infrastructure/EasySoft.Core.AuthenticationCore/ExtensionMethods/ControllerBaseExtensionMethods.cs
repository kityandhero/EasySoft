namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// ControllerBaseExtensionMethods
/// </summary>
public static class ControllerBaseExtensionMethods
{
    /// <summary>
    /// GetToken
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetToken(this ControllerBase controller, string key = "token")
    {
        return controller.HttpContext.Request.GetToken();
    }
}