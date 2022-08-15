using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class JobConfig : IConfig
{
    public static readonly JobConfig Instance = new();

    public string MaxThread { get; set; }

    public string TimeInterval { get; set; }

    public string SpecifiedHour { get; set; }

    public string SpecifiedMinute { get; set; }

    public string SpecifiedSecond { get; set; }

    public string CurtailHour { get; set; }

    public JobConfig()
    {
        MaxThread = "1";
        TimeInterval = "600";
        SpecifiedHour = "-1";
        SpecifiedMinute = "-1";
        SpecifiedSecond = "-1";
        CurtailHour = "-1";
    }
}