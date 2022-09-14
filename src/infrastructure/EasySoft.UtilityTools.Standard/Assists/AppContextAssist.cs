using System;
using System.IO;
using System.Reflection;

namespace EasySoft.UtilityTools.Standard.Assists;

public static class AppContextAssist
{
    public static string GetBaseDirectory()
    {
        var directory = AppContext.BaseDirectory;

        directory = directory.Replace("\\", "/");

        return directory;
    }

    public static string GetServerDirectory(string path)
    {
        var directory = $"${AppContext.BaseDirectory}\\{path}";

        directory = directory.Replace("\\", "/");

        return directory;
    }

    public static string GetTempDirectory()
    {
        return Path.GetTempPath();
    }
}