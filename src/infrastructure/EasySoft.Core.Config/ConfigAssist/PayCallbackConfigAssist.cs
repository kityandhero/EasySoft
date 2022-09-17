using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class PayCallbackConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(PayCallbackConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static PayCallbackConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(PayCallbackConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            true,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(PayCallbackConfig.Instance);
    }
    
    public static void Init()
    {
    }

    private static PayCallbackConfig GetConfig()
    {
        return PayCallbackConfig.Instance;
    }

    public static string GetCallbackHost()
    {
        var v = GetConfig().CallbackHost;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception("缺少支付回调配置（PayCallbackHost）");
        }

        return v;
    }
}