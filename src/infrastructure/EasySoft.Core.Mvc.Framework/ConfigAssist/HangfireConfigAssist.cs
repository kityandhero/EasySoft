using EasySoft.Core.Mvc.Framework.ConfigCollection;
using EasySoft.Core.Mvc.Framework.Utils;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ConfigAssist;

public static class HangfireConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(HangfireConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static HangfireConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(HangfireConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(HangfireConfig.Instance);
    }

    private static HangfireConfig GetConfig()
    {
        return HangfireConfig.Instance;
    }

    public static bool GetEnable()
    {
        var v = GetConfig().Enable;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置Swagger Enable: {ConfigFile} -> Enable,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static string GetStorage()
    {
        var v = GetConfig().Storage;

        v = v.Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(v))
        {
            throw new Exception(
                $"请配置Swagger Enable: {ConfigFile} -> Storage"
            );
        }

        return v;
    }

    public static WebApplication SetHangfire(WebApplication application)
    {
        if (!GetEnable())
        {
            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        return application;
    }
}