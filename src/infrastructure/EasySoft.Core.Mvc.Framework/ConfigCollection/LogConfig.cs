using EasySoft.Core.Mvc.Framework.ConfigInterface;

namespace EasySoft.Core.Mvc.Framework.ConfigCollection;

public class LogConfig : IConfig
{
    public static readonly LogConfig Instance = new();

    public string LogWeChatSessionToRemote { get; set; }

    public LogConfig()
    {
        LogWeChatSessionToRemote = "0";
    }
}