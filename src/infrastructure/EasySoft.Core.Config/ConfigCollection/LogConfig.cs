using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class LogConfig : IConfig
{
    public static readonly LogConfig Instance = new();

    public string LogWeChatSessionToRemote { get; set; }

    public LogConfig()
    {
        LogWeChatSessionToRemote = "0";
    }
}