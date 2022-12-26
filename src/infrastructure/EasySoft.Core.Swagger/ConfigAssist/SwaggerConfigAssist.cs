using EasySoft.Core.Swagger.ConfigCollection;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Swagger.ConfigAssist;

/// <summary>
/// SwaggerConfigAssist
/// </summary>
public static class SwaggerConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(SwaggerConfig).ToLowerFirst()}.json";

    private static readonly string FilePath;

    private static IConfiguration Configuration { get; set; }

    static SwaggerConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        FilePath = $"{directory}{nameof(SwaggerConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder();

        builder.AddMultiJsonFile(FilePath);

        Configuration = builder.Build();

        Configuration.Bind(SwaggerConfig.Instance);
    }

    /// <summary>
    /// Init
    /// </summary>
    public static void Init()
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(SwaggerConfigAssist)}.{nameof(Init)}."
        );
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
    /// GetConfigFileInfo
    /// </summary>
    /// <returns></returns>
    public static string GetConfigFileInfo()
    {
        return $"[{ConfigFile}](./configures/{ConfigFile})";
    }

    private static SwaggerConfig GetConfig()
    {
        return SwaggerConfig.Instance;
    }

    /// <summary>
    /// GetSwitch
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigErrorException"></exception>
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

    /// <summary>
    /// GetTitle
    /// </summary>
    /// <returns></returns>
    public static string GetTitle()
    {
        var v = GetConfig().Title;

        v = (string.IsNullOrWhiteSpace(v) ? Assembly.GetEntryAssembly()?.GetName().Name : v) ?? "";

        return v;
    }

    /// <summary>
    /// GetDescription
    /// </summary>
    /// <returns></returns>
    public static string GetDescription()
    {
        var v = GetConfig().Description;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetVersion
    /// </summary>
    /// <returns></returns>
    public static string GetVersion()
    {
        var v = GetConfig().Version;

        v = string.IsNullOrWhiteSpace(v) ? "1.0" : v;

        return v;
    }

    /// <summary>
    /// GetContactName
    /// </summary>
    /// <returns></returns>
    public static string GetContactName()
    {
        var v = GetConfig().ContactName;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetContactEmail
    /// </summary>
    /// <returns></returns>
    public static string GetContactEmail()
    {
        var v = GetConfig().ContactEmail;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetContactUrl
    /// </summary>
    /// <returns></returns>
    public static string GetContactUrl()
    {
        var v = GetConfig().ContactUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetLicenseName
    /// </summary>
    /// <returns></returns>
    public static string GetLicenseName()
    {
        var v = GetConfig().LicenseName;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetLicenseUrl
    /// </summary>
    /// <returns></returns>
    public static string GetLicenseUrl()
    {
        var v = GetConfig().LicenseUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetOpenApiServerUrl
    /// </summary>
    /// <returns></returns>
    public static string GetOpenApiServerUrl()
    {
        var v = GetConfig().OpenApiServerUrl;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }

    /// <summary>
    /// GetOpenApiServerDescription
    /// </summary>
    /// <returns></returns>
    public static string GetOpenApiServerDescription()
    {
        var v = GetConfig().OpenApiServerDescription;

        v = string.IsNullOrWhiteSpace(v) ? "" : v;

        return v;
    }
}