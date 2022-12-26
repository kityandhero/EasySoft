namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// consul 配置中心设置
/// </summary>
public class ConsulRegistrationCenterConfig
{
    /// <summary>
    /// Instance
    /// </summary>
    public static readonly ConsulRegistrationCenterConfig Instance = new();

    /// <summary>
    /// 配置中心地址
    /// </summary>
    public string CenterAddress { get; set; } = "";

    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; } = "";

    /// <summary>
    /// 服务 IP
    /// </summary>
    public string ServiceIP { get; set; } = "";

    /// <summary>
    /// 服务端口
    /// </summary>
    public string ServicePort { get; set; } = "80";

    /// <summary>
    /// 服务停止多久后进行注销 (秒), 默认值 5
    /// </summary>
    public string DeregisterCriticalServiceAfter { get; set; } = "5";

    /// <summary>
    /// 健康检查间隔,心跳间隔 (秒), 默认值 10
    /// </summary>
    public string HealthCheckIntervalInSecond { get; set; } = "10";

    /// <summary>
    /// 健康检测
    /// </summary>
    public string ServiceHealthCheck { get; set; } = "";

    /// <summary>
    /// 超时时间 (秒), 默认值 5
    /// </summary> 
    public string Timeout { get; set; } = "5";
}