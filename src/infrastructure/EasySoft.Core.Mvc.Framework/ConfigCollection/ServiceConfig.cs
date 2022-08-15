using EasySoft.Core.Mvc.Framework.ConfigInterface;

namespace EasySoft.Core.Mvc.Framework.ConfigCollection;

public class ServiceConfig : IConfig
{
    public static readonly ServiceConfig Instance = new();

    public string Prefix { get; set; }

    public ServiceConfig()
    {
        Prefix = "PandoraMulti";
    }
}