using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public static class GeneralConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(GeneralConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static GeneralConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{ConfigFile}";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(GeneralConfig.Instance);
    }

    public static void Init()
    {
    }

    private static GeneralConfig GetConfig()
    {
        return GeneralConfig.Instance;
    }

    public static bool GetUseStaticFiles()
    {
        var v = GetConfig().UseStaticFiles;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 UseStaticFiles: {ConfigFile} -> UseStaticFiles,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static bool GetUseAuthentication()
    {
        var v = GetConfig().UseAuthentication;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 UseAuthentication: {ConfigFile} -> UseAuthentication,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static bool GetUseAuthorization()
    {
        var v = GetConfig().UseAuthorization;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 UseAuthorization: {ConfigFile} -> UseAuthorization,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static bool GetCorsEnable()
    {
        var v = GetConfig().CorsEnable;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 CorsEnable: {ConfigFile} -> CorsEnable,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static List<string> GetCorsPolicies()
    {
        var v = GetConfig().CorsPolicies.Trim()
            .Split(",")
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToList();

        return v;
    }
}