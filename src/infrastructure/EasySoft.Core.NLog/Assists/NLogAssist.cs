using NLog;

namespace EasySoft.Core.NLog.Assists;

public static class NLogAssist
{
    public static void ReloadConfiguration()
    {
        LogManager.Configuration.Reload();
    }
}