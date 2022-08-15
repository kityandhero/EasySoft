using Framework.ConfigCollection;
using Framework.Utils;
using Microsoft.Extensions.Configuration;
using UtilityTools.ExtensionMethods;

namespace Framework.ConfigAssist;

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