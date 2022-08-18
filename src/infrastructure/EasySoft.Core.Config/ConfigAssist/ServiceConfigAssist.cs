﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class ServiceConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(ServiceConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static ServiceConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(ServiceConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(ServiceConfig.Instance);
    }
    
    public static void Init()
    {
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

        return string.IsNullOrWhiteSpace(v) ? "PandoraMulti" : v;
    }
}