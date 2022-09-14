namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulConfig
{
    public string ConsulAddress { get; set; }

    public string ServiceName { get; set; }

    public string ServiceIP { get; set; }

    public string ServicePort { get; set; }

    public string ServiceHealthCheck { get; set; }

    public static readonly ConsulConfig Instance = new();

    public ConsulConfig()
    {
        ConsulAddress = "";
        ServiceName = "";
        ServiceIP = "";
        ServicePort = "80";
        ServiceHealthCheck = "";
    }
}