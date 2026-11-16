namespace EasySoft.Core.Config.Utils;

/// <summary>
/// Tools
/// </summary>
public static class Tools
{
    /// <summary>
    /// GetConfigureDirectory
    /// </summary>
    /// <returns></returns>
    public static string GetConfigureDirectory()
    {
        var configureFolderPath = $"{AppContextAssist.GetBaseDirectory()}/configures/";

        return configureFolderPath;
    }
}