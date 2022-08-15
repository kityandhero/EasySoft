using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public class SwaggerConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(SwaggerConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static SwaggerConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(SwaggerConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(SwaggerConfig.Instance);
    }

    private static SwaggerConfig GetConfig()
    {
        return SwaggerConfig.Instance;
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
}