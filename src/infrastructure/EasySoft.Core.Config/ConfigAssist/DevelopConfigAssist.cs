﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class DevelopConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(DevelopConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static DevelopConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(DevelopConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(DevelopConfig.Instance);
    }

    public static void Init()
    {
    }

    private static DevelopConfig GetConfig()
    {
        return DevelopConfig.Instance;
    }

    public static bool GetElasticSearchDataVersion()
    {
        var v = GetConfig().DevelopMode.Remove(" ").Trim();

        return v == "1";
    }
}