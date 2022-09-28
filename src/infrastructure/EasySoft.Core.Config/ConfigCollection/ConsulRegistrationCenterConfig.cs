namespace EasySoft.Core.Config.ConfigCollection;

public class ConsulRegistrationCenterConfig
{
    public string CenterAddress { get; set; }

    public string ServiceName { get; set; }

    public string ServiceIP { get; set; }

    public string ServicePort { get; set; }

    /// <summary>
    /// 服务停止多久后进行注销 (秒), 默认值 5
    /// </summary>
    public string DeregisterCriticalServiceAfter { get; set; }

    /// <summary>
    /// 健康检查间隔,心跳间隔 (秒), 默认值 10
    /// </summary>
    public string HealthCheckIntervalInSecond { get; set; }

    public string ServiceHealthCheck { get; set; }

    /// <summary>
    /// 超时时间 (秒), 默认值 5
    /// </summary> 
    public string Timeout { get; set; }

    public static readonly ConsulRegistrationCenterConfig Instance = new();

    public ConsulRegistrationCenterConfig()
    {
        CenterAddress = "";
        ServiceName = "";
        ServiceIP = "";
        ServicePort = "80";
        DeregisterCriticalServiceAfter = "5";
        HealthCheckIntervalInSecond = "10";
        Timeout = "5";
        ServiceHealthCheck = "";
    }
}