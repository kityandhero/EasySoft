using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class DevelopConfig : IConfig
{
    public static readonly DevelopConfig Instance = new();

    public string DevelopMode { get; set; }

    public DevelopConfig()
    {
        DevelopMode = "0";
    }
}