using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class LogConfig : IConfig
{
    public static readonly LogConfig Instance = new();

    public string LogWeChatSessionToRemote { get; set; }

    public LogConfig()
    {
        LogWeChatSessionToRemote = "0";
    }
}