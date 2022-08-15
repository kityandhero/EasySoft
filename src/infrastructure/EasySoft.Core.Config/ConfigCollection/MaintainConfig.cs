using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class MaintainConfig : IConfig
{
    public static readonly MaintainConfig Instance = new();

    public string UrlPollingRequests { get; set; }

    public MaintainConfig()
    {
        UrlPollingRequests = "";
    }
}