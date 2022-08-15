using EasySoft.Core.Mvc.Framework.ConfigInterface;

namespace EasySoft.Core.Mvc.Framework.ConfigCollection;

public class BusinessConfig : IConfig
{
    public static readonly BusinessConfig Instance = new();

    /// <summary>
    /// 出库预统计运行持续时间（分）,默认值为30
    /// </summary>
    public string BeforehandOutboundStatisticJobSustainMinutes { get; set; }

    public BusinessConfig()
    {
        BeforehandOutboundStatisticJobSustainMinutes = "30";
    }
}