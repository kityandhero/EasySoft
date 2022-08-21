using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class GeneralConfig : IConfig
{
    public static readonly GeneralConfig Instance = new();

    public string CacheMode { get; set; }

    public string AccessWayDetectSwitch { get; set; }

    public string RemoteGeneralLogSwitch { get; set; }

    public string RemoteErrorLogSwitch { get; set; }

    public string UseStaticFiles { get; set; }

    public string UseAuthentication { get; set; }

    public string UseAuthorization { get; set; }

    public string CorsSwitch { get; set; }

    public string CorsPolicies { get; set; }

    public GeneralConfig()
    {
        CacheMode = "InMemory";
        AccessWayDetectSwitch = "0";
        RemoteGeneralLogSwitch = "0";
        RemoteErrorLogSwitch = "0";
        UseStaticFiles = "1";
        UseAuthentication = "0";
        UseAuthorization = "0";
        CorsSwitch = "0";
        CorsPolicies = "*";
    }
}