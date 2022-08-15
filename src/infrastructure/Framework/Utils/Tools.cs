namespace Framework.Utils;

public static class Tools
{
    public static string GetBaseDirectory()
    {
        var directory = AppContext.BaseDirectory;

        directory = directory.Replace("\\", "/");

        return directory;
    }

    public static string GetConfigureDirectory()
    {
        var configureFolderPath = $"{GetBaseDirectory()}/configures/";

        return configureFolderPath;
    }
}