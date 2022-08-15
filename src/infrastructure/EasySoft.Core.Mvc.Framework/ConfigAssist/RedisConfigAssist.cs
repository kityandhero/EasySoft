using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

public static class RedisConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(RedisConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static RedisConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(RedisConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(RedisConfig.Instance);
    }

    private static RedisConfig GetConfig()
    {
        return RedisConfig.Instance;
    }

    public static int GetActivateMode()
    {
        var v = GetConfig().ActivateMode;

        if (!v.IsInt() || !v.ToInt().CheckInEnum<IPAssist.Mode>())
        {
            throw new Exception(
                $"请配置Redis ActivateMode: {ConfigFile} -> ActivateMode"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 获取服务配应用安装前缀配置
    /// </summary>
    public static string GetConnection()
    {
        var v = GetConfig().Connection;

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置Redis Connection: {ConfigFile} -> Connection"
            );
        }

        return v;
    }

    public static string GetKeyPrefix()
    {
        var v = GetConfig().KeyPrefix;

        v = v.Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(v) ? "PandoraMulti" : v;
    }
}