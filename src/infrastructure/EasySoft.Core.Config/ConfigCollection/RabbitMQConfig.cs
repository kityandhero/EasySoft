using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class RabbitMQConfig : IConfig
{
    public static readonly RabbitMQConfig Instance = new();

    public string HostName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string VirtualHost { get; set; }

    public string ConnectionTimeout { get; set; }

    public RabbitMQConfig()
    {
        HostName = "";
        UserName = "";
        Password = "";
        VirtualHost = "/";
        ConnectionTimeout = "30";
    }
}