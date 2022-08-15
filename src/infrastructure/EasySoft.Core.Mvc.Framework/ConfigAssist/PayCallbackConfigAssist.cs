using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

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
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(PayCallbackConfig.Instance);
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