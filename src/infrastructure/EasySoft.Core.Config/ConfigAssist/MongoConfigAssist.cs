﻿using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.ConfigAssist;

/// <summary>
/// MongoConfigAssist
/// </summary>
public static class MongoConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(MongoConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static MongoConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(MongoConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(MongoConfig.Instance);
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
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static MongoConfig GetConfig()
    {
        return MongoConfig.Instance;
    }

    /// <summary>
    /// GetConnection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetConnection()
    {
        var v = GetConfig().Connection;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Mongo Connection: {ConfigFile} -> Connection",
                GetConfigFileInfo()
            );

        return v;
    }

    /// <summary>
    /// GetDatabase
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
    public static string GetDatabase()
    {
        var v = GetConfig().Database;

        if (string.IsNullOrWhiteSpace(v))
            throw new ConfigErrorException(
                $"请配置Mongo Database: {ConfigFile} -> Database",
                GetConfigFileInfo()
            );

        return v.Remove(" ").Trim();
    }
}