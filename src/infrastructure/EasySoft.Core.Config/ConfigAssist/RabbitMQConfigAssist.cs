using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class RabbitMQConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(RabbitMQConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static RabbitMQConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(RabbitMQConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(RabbitMQConfig.Instance);
    }

    public static void Init()
    {
    }

    private static RabbitMQConfig GetConfig()
    {
        return RabbitMQConfig.Instance;
    }

    public static string GetHostName()
    {
        var v = GetConfig().HostName.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置RabbitMQ消息队列HostName项: {ConfigFile} -> HostName"
            );
        }

        return v;
    }

    public static string GetUserName()
    {
        var v = GetConfig().UserName.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置RabbitMQ消息队列UserName项: {ConfigFile} -> UserName"
            );
        }

        return v;
    }

    public static string GetPassword()
    {
        var v = GetConfig().Password.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置RabbitMQ消息队列Password项: {ConfigFile} -> Password"
            );
        }

        return v;
    }

    public static string GetVirtualHost()
    {
        var v = GetConfig().VirtualHost.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置RabbitMQ消息队列VirtualHost项: {ConfigFile} -> VirtualHost"
            );
        }

        return v;
    }

    public static int GetConnectionTimeout()
    {
        var v = GetConfig().ConnectionTimeout.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() < 0)
        {
            throw new Exception(
                $"请配置RabbitMQ消息队列ConnectionTimeout项: {ConfigFile} -> ConnectionTimeout"
            );
        }

        return v.ToInt();
    }
}