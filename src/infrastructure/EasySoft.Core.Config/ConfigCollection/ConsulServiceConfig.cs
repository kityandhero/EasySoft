namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulServiceConfig
{
    public string ServiceName { get; set; }

    public string ServiceIP { get; set; }

    public string ServicePort { get; set; }

    public string ServiceHealthCheck { get; set; }

    public static readonly ConsulServiceConfig Instance = new();

    public ConsulServiceConfig()
    {
        ServiceName = "";
        ServiceIP = "";
        ServicePort = "80";
        ServiceHealthCheck = "";
    }
}