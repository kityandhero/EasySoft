using Framework.ConfigInterface;
using UtilityTools.Assists;

namespace Framework.ConfigCollection;

public class RedisConfig : IConfig
{
    public static readonly RedisConfig Instance = new();

    public string KeyPrefix { get; set; }

    public string Connection { get; set; }

    public string ActivateMode { get; set; }

    public RedisConfig()
    {
        Connection = "";
        KeyPrefix = "PandoraMulti";
        ActivateMode = ((int)IPAssist.Mode.Intranet).ToString();
    }
}