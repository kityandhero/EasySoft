using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
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

    public static bool GetRemoteLogSwitch()
    {
        return GetRemoteErrorLogSwitch() || GetRemoteGeneralLogSwitch();
    }

    private static GeneralConfig GetConfig()
    {
        return GeneralConfig.Instance;
    }

    public static string GetCacheMode()
    {
        var v = GetConfig().CacheMode;

        v = string.IsNullOrWhiteSpace(v) ? "InMemory" : v;

        if (!v.In("InMemory", "Redis"))
        {
            throw new Exception(
                $"请配置 CacheMode: {ConfigFile} -> CacheMode,请设置 InMemory/Redis"
            );
        }

        return v;
    }

    public static bool GetAccessWayDetectSwitch()
    {
        var v = GetConfig().AccessWayDetectSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 AccessWayDetectSwitch: {ConfigFile} -> AccessWayDetectSwitch,请设置 0/1,开启后将在请求进入时校验持久数据存在性"
            );
        }

        if (v.ToInt() == 1)
        {
            if (!FlagAssist.IdentityVerificationSwitch)
            {
                throw new Exception(
                    "AccessWayDetectSwitch work with UseAdvanceIdentityVerification, if you do not use UseAdvanceIdentityVerification, do not set it to enable"
                );
            }
        }

        return v.ToInt() == 1;
    }

    public static bool GetRemoteGeneralLogSwitch()
    {
        var v = GetConfig().RemoteGeneralLogSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 RemoteGeneralLogSwitch: {ConfigFile} -> RemoteGeneralLogSwitch,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
    }

    public static bool GetRemoteErrorLogSwitch()
    {
        var v = GetConfig().RemoteErrorLogSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 RemoteErrorLogSwitch: {ConfigFile} -> RemoteErrorLogSwitch,请设置 0/1"
            );
        }

        return v.ToInt() == 1;
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

    public static bool GetCorsSwitch()
    {
        var v = GetConfig().CorsSwitch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
        {
            throw new Exception(
                $"请配置 CorsSwitch: {ConfigFile} -> CorsSwitch,请设置 0/1"
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