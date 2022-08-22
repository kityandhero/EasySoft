using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public class BusinessConfigAssist
{
    // ReSharper disable once UnusedMember.Local
    private static readonly string ConfigFile = $"{nameof(BusinessConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static BusinessConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(BusinessConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(BusinessConfig.Instance);
    }
    
    public static void Init()
    {
    }

    private static BusinessConfig GetConfig()
    {
        return BusinessConfig.Instance;
    }

    /// <summary>
    /// 出库预统计运行持续时间（分）,默认值为30
    /// </summary>
    public static int GetBeforehandOutboundStatisticJobSustainMinutes()
    {
        var v = GetConfig().BeforehandOutboundStatisticJobSustainMinutes;

        v = string.IsNullOrWhiteSpace(v)
            ? "30"
            : v;

        if (!v.IsInt())
        {
            throw new Exception(
                "无效的出库预统计运行持续时间（分）配置（BeforehandOutboundStatisticJobSustainMinutes）,请设置数字"
            );
        }

        var minutes = v.ToInt();

        if (minutes is < 10 or > 60)
        {
            throw new Exception(
                "无效的出库预统计运行持续时间（分）配置（BeforehandOutboundStatisticJobSustainMinutes）,有效值区间为 10 ~ 60"
            );
        }

        return v.ToInt();
    }
}