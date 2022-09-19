namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulRegistrationCenterConfig
{
    public string CenterAddress { get; set; }

    public string ServiceName { get; set; }

    public string ServiceIP { get; set; }

    public string ServicePort { get; set; }

    public string ServiceHealthCheck { get; set; }

    public static readonly ConsulRegistrationCenterConfig Instance = new();

    public ConsulRegistrationCenterConfig()
    {
        CenterAddress = "";
        ServiceName = "";
        ServiceIP = "";
        ServicePort = "80";
        ServiceHealthCheck = "";
    }
}