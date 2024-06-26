﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// RabbitMQConfigAssist
/// </summary>
public static class RabbitMQConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(RabbitMQConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static RabbitMQConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(RabbitMQConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(RabbitMQConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
    }

    /// <summary>
    /// 获取配置文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFilePath()
    {
        return FilePath;
    }

    /// <summary>
    /// 获取配置文件内容
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetConfigFileContent()
    {
        var content = await GetConfigFilePath().ReadFile();

        return string.IsNullOrWhiteSpace(content) ? content : JsonConvertAssist.FormatText(content);
    }

    /// <summary>
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[generalConfig.json](./configures/{ConfigFile})";
    }

    private static RabbitMQConfig GetConfig()
    {
        return RabbitMQConfig.Instance;
    }

    /// <summary>
    /// 获取域名
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetHostName()
    {
        var v = GetConfig().HostName.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置RabbitMQ消息队列HostName项: {ConfigFile} -> HostName",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 获取用户名
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetUserName()
    {
        var v = GetConfig().UserName.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置RabbitMQ消息队列UserName项: {ConfigFile} -> UserName",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 获取密码
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetPassword()
    {
        var v = GetConfig().Password.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置RabbitMQ消息队列Password项: {ConfigFile} -> Password",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 获取VirtualHost
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetVirtualHost()
    {
        var v = GetConfig().VirtualHost.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置RabbitMQ消息队列VirtualHost项: {ConfigFile} -> VirtualHost",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// 获取端口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static int GetPort()
    {
        var v = GetConfig().Port.Remove(" ").Trim();

        v = string.IsNullOrWhiteSpace(v) ? "5672" : v;

        if (!v.IsInt(out var value) || value < 0)
            throw new ConfigErrorException(
                $"请配置 Port: {ConfigFile} -> Port,请设置数字 value > 0",
                GetConfigFileInfo()
            );

        return value;
    }

    /// <summary>
    /// 获取超时时间
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static int GetConnectionTimeout()
    {
        var v = GetConfig().ConnectionTimeout.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() < 0)
            throw new ConfigErrorException(
                $"请配置RabbitMQ消息队列ConnectionTimeout项: {ConfigFile} -> ConnectionTimeout",
                GetConfigFileInfo()
            );

        return v.ToInt();
    }
}