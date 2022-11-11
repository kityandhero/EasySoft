namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class ControllerBaseExtensionMethods
{
    public static string GetToken(this ControllerBase controller, string key = "token")
    {
        return controller.HttpContext.Request.GetToken();
    }

    public static async Task<string> GetTokenAsync(this ControllerBase controller, string key = "token")
    {
        return await controller.HttpContext.Request.GetTokenAsync();
    }
}