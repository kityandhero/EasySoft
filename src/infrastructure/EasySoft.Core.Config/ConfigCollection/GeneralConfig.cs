using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    /// <summary>
    /// ElasticSearch数据版本
    /// </summary>
    public string CorsEnable { get; set; }

    public string CorsPolicies { get; set; }

    public GeneralConfig()
    {
        CorsEnable = "0";
        CorsPolicies = "*";
    }
}