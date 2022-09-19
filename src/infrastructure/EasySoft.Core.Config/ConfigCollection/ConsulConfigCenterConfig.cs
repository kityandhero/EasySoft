namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulConfigCenterConfig
{
    public string CenterAddress { get; set; }

    public static readonly ConsulConfigCenterConfig Instance = new();

    public ConsulConfigCenterConfig()
    {
        CenterAddress = "";
    }
}