using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class ServiceConfig : IConfig
{
    public static readonly ServiceConfig Instance = new();

    public string Prefix { get; set; }

    public ServiceConfig()
    {
        Prefix = "PandoraMulti";
    }
}