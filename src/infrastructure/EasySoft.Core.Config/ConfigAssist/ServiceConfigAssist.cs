﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// ServiceConfigAssist
/// </summary>
public static class ServiceConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(ServiceConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static ServiceConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ServiceConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(ServiceConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
    }

    /// <summary>
    /// 获取配置文件信息
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static ServiceConfig GetConfig()
    {
        return ServiceConfig.Instance;
    }

    /// <summary>
    /// 获取服务配应用安装前缀配置
    /// </summary>
    public static string GetServicePrefix()
    {
        var v = GetConfig().Prefix;

        v = v.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "" : v;
    }
}