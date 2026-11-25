namespace EasySoft.Core.AdvanceNLog.Assists;

public static class LogManagerAssist
{
    private static string _prevRemoteNLogJsonConfig = "";

    public static void ReloadConfiguration()
    {
        LogManager.Configuration?.Reload();
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