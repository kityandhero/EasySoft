using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.UtilityTools.AspNetCore.Assists;

/// <summary>
/// WebHostAssist
/// </summary>
public static class WebHostAssist
{
    /// <summary>
    /// GetServerPath
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetServerPath(string path)
    {
        return ServiceProviderAssist.GetProvider().GetRequiredService<IWebHostEnvironment>().ContentRootPath + path;
    }
}