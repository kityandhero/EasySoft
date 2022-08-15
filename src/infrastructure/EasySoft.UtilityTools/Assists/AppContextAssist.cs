using System;

namespace EasySoft.UtilityTools.Assists;

public static class AppContextAssist
{
    public static string GetBaseDirectory()
    {
        var directory = AppContext.BaseDirectory;

        directory = directory.Replace("\\", "/");

        return directory;
    }
}