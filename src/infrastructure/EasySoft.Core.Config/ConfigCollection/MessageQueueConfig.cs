using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class MessageQueueConfig : IConfig
{
    public static readonly MessageQueueConfig Instance = new();

    public string HostName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string VirtualHost { get; set; }

    public string ConnectionTimeout { get; set; }

    public string Prefix { get; set; }

    public MessageQueueConfig()
    {
        HostName = "";
        UserName = "";
        Password = "";
        VirtualHost = "/";
        ConnectionTimeout = "30";
        Prefix = "";
    }
}