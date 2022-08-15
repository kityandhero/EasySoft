using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class MaintainConfig : IConfig
{
    public static readonly MaintainConfig Instance = new();

    public string UrlPollingRequests { get; set; }

    public MaintainConfig()
    {
        UrlPollingRequests = "";
    }
}