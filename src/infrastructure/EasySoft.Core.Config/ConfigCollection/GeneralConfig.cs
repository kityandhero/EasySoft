using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string UseStaticFiles { get; set; }

    public string UseAuthentication { get; set; }

    public string UseAuthorization { get; set; }

    public string CorsEnable { get; set; }

    public string CorsPolicies { get; set; }

    public GeneralConfig()
    {
        UseStaticFiles = "1";
        UseAuthentication = "0";
        UseAuthorization = "0";
        CorsEnable = "0";
        CorsPolicies = "*";
    }
}