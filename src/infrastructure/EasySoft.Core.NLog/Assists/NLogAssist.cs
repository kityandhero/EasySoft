using NLog;

namespace EasySoft.Core.NLog.Assists;

public static class NLogAssist
{
    private static string _prevRemoteNLogJsonConfig = "";

    public static void ReloadConfiguration()
    {
        LogManager.Configuration.Reload();
    }

    public static bool CheckChange(string remoteNLogJsonConfig)
    {
        if (_prevRemoteNLogJsonConfig == remoteNLogJsonConfig)
        {
            return false;
        }

        _prevRemoteNLogJsonConfig = remoteNLogJsonConfig;

        return true;
    }
}