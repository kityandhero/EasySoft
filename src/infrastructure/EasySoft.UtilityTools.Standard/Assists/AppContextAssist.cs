namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// AppContextAssist
/// </summary>
public static class AppContextAssist
{
    /// <summary>
    /// GetBaseDirectory
    /// </summary>
    /// <returns></returns>
    public static string GetBaseDirectory()
    {
        var directory = AppContext.BaseDirectory;

        directory = directory.Replace("\\", "/");

        return directory;
    }

    /// <summary>
    /// GetServerDirectory
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetServerDirectory(string path)
    {
        var directory = $"${AppContext.BaseDirectory}\\{path}";

        directory = directory.Replace("\\", "/");

        return directory;
    }

    /// <summary>
    /// GetTempDirectory
    /// </summary>
    /// <returns></returns>
    public static string GetTempDirectory()
    {
        return Path.GetTempPath();
    }
}