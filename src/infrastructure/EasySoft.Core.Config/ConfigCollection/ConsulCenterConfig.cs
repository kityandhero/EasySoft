namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulCenterConfig
{
    public string CenterAddress { get; set; }

    public static readonly ConsulCenterConfig Instance = new();

    public ConsulCenterConfig()
    {
        CenterAddress = "";
    }
}