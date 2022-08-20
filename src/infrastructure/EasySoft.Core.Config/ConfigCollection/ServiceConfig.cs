using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class ServiceConfig : IConfig
{
    public static readonly ServiceConfig Instance = new();

    public string Prefix { get; set; }

    public ServiceConfig()
    {
        Prefix = "";
    }
}