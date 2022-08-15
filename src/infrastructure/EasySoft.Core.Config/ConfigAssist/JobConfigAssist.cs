using EasySoft.Core.Config.ConfigCollection;
using EasySoft.Core.Config.Utils;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Config.ConfigAssist;

public class JobConfigAssist
{
    private static readonly string ConfigFile = $"{nameof(JobConfig).ToLowerFirst()}.json";

    private static IConfiguration Configuration { get; set; }

    static JobConfigAssist()
    {
        var directory = Tools.GetConfigureDirectory();

        var filePath = $"{directory}{nameof(JobConfig).ToLowerFirst()}.json";

        var builder = new ConfigurationBuilder().AddJsonFile(
            filePath,
            false,
            true
        );

        Configuration = builder.Build();

        Configuration.Bind(JobConfig.Instance);
    }

    private static JobConfig GetConfig()
    {
        return JobConfig.Instance;
    }

    /// <summary>
    /// 并行线程数
    /// </summary>
    public static int GetMaxThread()
    {
        var v = GetConfig().MaxThread.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() <= 0)
        {
            throw new Exception(
                $"请配置Job MaxThread: {ConfigFile} -> MaxThread"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 执行时间间隔 默认600秒
    /// </summary>
    public static long GetTimeInterval()
    {
        var v = GetConfig().TimeInterval.Remove(" ").Trim();

        if (!v.IsInt64() || v.ToInt64() <= 0)
        {
            throw new Exception(
                $"请配置Job TimeInterval: {ConfigFile} -> TimeInterval"
            );
        }

        return v.ToInt64();
    }

    /// <summary>
    /// 指定的时间（小时）
    /// </summary>
    public static int GetSpecifiedHour()
    {
        var v = GetConfig().SpecifiedHour.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() < 0 || v.ToInt() >= 24)
        {
            throw new Exception(
                $"请配置Job SpecifiedHour: {ConfigFile} -> SpecifiedHour, 可配置范围: [0 ~ 23]"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 指定的时间（分钟）
    /// </summary>
    public static int GetSpecifiedMinute()
    {
        var v = GetConfig().SpecifiedMinute.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() <= 0 || v.ToInt() >= 59)
        {
            throw new Exception(
                $"请配置Job SpecifiedMinute: {ConfigFile} -> SpecifiedMinute, 可配置范围: [0 ~ 59]"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 指定的时间（秒）
    /// </summary>
    public static int GetSpecifiedSecond()
    {
        var v = GetConfig().SpecifiedSecond.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() <= 0)
        {
            throw new Exception(
                $"请配置Job SpecifiedSecond: {ConfigFile} -> SpecifiedSecond, 可配置范围: [0 ~ 59]"
            );
        }

        return v.ToInt();
    }

    /// <summary>
    /// 偏差的小时数（0为无偏差，大于1为向前推指定小时数，例如1代表当前时间前推一小时）
    /// </summary>
    public static int GetCurtailHour()
    {
        var v = GetConfig().CurtailHour.Remove(" ").Trim();

        if (!v.IsInt() || v.ToInt() < 0)
        {
            throw new Exception(
                $"请配置Job CurtailHour: {ConfigFile} -> CurtailHour"
            );
        }

        return v.ToInt();
    }
}