using System.Reflection;
using EasySoft.Core.Config.Exceptions;
using EasySoft.Core.Config.ExtensionMethods;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.Swagger.ConfigCollection;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Swagger.ConfigAssist;

public static class SwaggerConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(SwaggerConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static SwaggerConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(SwaggerConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(filePath);

        Configuration = builder.Build();

        Configuration.Bind(SwaggerConfig.Instance);
    }

    public static void Init()
    {
    }

    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static SwaggerConfig GetConfig()
    {
        return SwaggerConfig.Instance;
    }

    public static bool GetSwitch()
    {
        var v = GetConfig().Switch;

        v = string.IsNullOrWhiteSpace(v) ? "0" : v;

        if (!v.IsInt())
            throw new ConfigErrorException(
                $"请配置Swagger Enable: {ConfigFile} -> Enable,请设置 0/1",
                GetConfigFileInfo()
            );

        return v.ToInt() == 1;
    }

    public static string GetTitle()
    {
        var v = GetConfig().Title;

        v = (string.IsNullOrWhiteSpace(v) ? Assembly.GetEntryAssembly()?.GetName().Name : v) ?? "";

        return v;
    }

    public static string GetDescription()
    {
        var v = GetConfig().Description;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetVersion()
    {
        var v = GetConfig().Version;

        v = string.IsNullOrWhiteSpace(v) ? "1.0" : v;

        return v;
    }

    public static string GetContactName()
    {
        var v = GetConfig().ContactName;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetContactEmail()
    {
        var v = GetConfig().ContactEmail;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetContactUrl()
    {
        var v = GetConfig().ContactUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetLicenseName()
    {
        var v = GetConfig().LicenseName;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetLicenseUrl()
    {
        var v = GetConfig().LicenseUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetOpenApiServerUrl()
    {
        var v = GetConfig().OpenApiServerUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    public static string GetOpenApiServerDescription()
    {
        var v = GetConfig().OpenApiServerDescription;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}