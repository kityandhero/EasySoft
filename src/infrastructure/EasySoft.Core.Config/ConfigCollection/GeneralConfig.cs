using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string UseAuthentication { get; set; }

    public string UseAuthorization { get; set; }

    public string CorsEnable { get; set; }

    public string CorsPolicies { get; set; }

    public GeneralConfig()
    {
        UseAuthentication = "0";
        UseAuthorization = "0";
        CorsEnable = "0";
        CorsPolicies = "*";
    }
}