using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

public static class MessageQueueConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(MessageQueueConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static MessageQueueConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(MessageQueueConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(MessageQueueConfig.Instance);
    }

    private static MessageQueueConfig GetConfig()
    {
        return MessageQueueConfig.Instance;
    }

    public static string GetHostName()
    {
        var v = GetConfig().HostName.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置消息队列HostName项: {ConfigFile} -> HostName"
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
                $"请配置消息队列UserName项: {ConfigFile} -> UserName"
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
                $"请配置消息队列Password项: {ConfigFile} -> Password"
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
                $"请配置消息队列VirtualHost项: {ConfigFile} -> VirtualHost"
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
                $"请配置消息队列ConnectionTimeout项: {ConfigFile} -> ConnectionTimeout"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 获取服务配应用安装前缀配置
    /// </summary>
    public static string GetPrefix()
    {
        var v = GetConfig().Prefix.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "PandoraMulti" : v;
    }
}