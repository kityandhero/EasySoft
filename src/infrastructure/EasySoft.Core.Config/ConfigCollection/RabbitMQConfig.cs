using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

/// <summary>
/// RabbitMQConfig
/// </summary>
public class RabbitMQConfig : IConfig
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static readonly RabbitMQConfig Instance = new();

    /// <summary>
    /// HostName
    /// </summary>
    public string HostName { get; set; } = "";

    /// <summary>
    /// HostName
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    /// VirtualHost
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    /// <summary>
    /// Port
    /// </summary>
    public string Port { get; set; } = "5672";

    /// <summary>
    /// ConnectionTimeout
    /// </summary>
    public string ConnectionTimeout { get; set; } = "30";
}