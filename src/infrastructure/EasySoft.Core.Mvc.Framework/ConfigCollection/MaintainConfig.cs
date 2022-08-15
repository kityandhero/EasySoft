using EasySoft.Core.Mvc.Framework.ConfigInterface;

namespace EasySoft.Core.Mvc.Framework.ConfigCollection;

public class MaintainConfig : IConfig
{
    public static readonly MaintainConfig Instance = new();

    public string UrlPollingRequests { get; set; }

    public MaintainConfig()
    {
        UrlPollingRequests = "";
    }
}