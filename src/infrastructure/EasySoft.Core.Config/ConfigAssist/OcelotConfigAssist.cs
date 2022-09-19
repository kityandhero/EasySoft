﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class OcelotConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(OcelotConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static OcelotConfigAssist()
    {
        var directory = AppContextAssist.GetBaseDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        ).AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public static void Init()
    {
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    public static IConfiguration GetConfiguration()
    {
        return Configuration;
    }
}