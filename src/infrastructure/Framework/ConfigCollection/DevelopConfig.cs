using Framework.ConfigInterface;

namespace Framework.ConfigCollection;

public class DevelopConfig : IConfig
{
    public static readonly DevelopConfig Instance = new();

    public string DevelopMode { get; set; }

    public DevelopConfig()
    {
        DevelopMode = "0";
    }
}