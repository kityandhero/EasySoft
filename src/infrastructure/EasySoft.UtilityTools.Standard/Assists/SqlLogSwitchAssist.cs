using System.Threading;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// SqlLogSwitchAssist
/// </summary>
public static class SqlLogSwitchAssist
{
    private static Timer _timer;

    private static bool _switch = false;

    /// <summary>
    /// 定时检测更新，比如在使用 In Memory Cache 情况下保证正确执行抓取日志
    /// </summary>
    private static Func<IChannel, bool>? _timingDetectAction = null;

    static SqlLogSwitchAssist()
    {
        _timer = new Timer(
            state =>
            {
                var time = DateTime.Now;

                var currentSecond = time.Second;

                if (currentSecond % 10 != 0)
                {
                    return;
                }

                if (_timingDetectAction == null)
                {
                    return;
                }

                try
                {
                    var channel = ChannelAssist.GetCurrentChannel();

                    if (channel.Value == Channel.Unknown.Value)
                    {
                        return;
                    }

                    var result = _timingDetectAction(ChannelAssist.GetCurrentChannel());

                    Console.WriteLine($"Timing detect sql log switch: {time.ToYearMonthDayHourMinute()}");

                    SetCurrentSwitch(result);
                }
                catch (Exception)
                {
                    // ignored
                }
            },
            null,
            1000,
            1000
        );
    }

    /// <summary>  
    /// 设置当前存储模式
    /// </summary>
    /// <param name="timingDetectAction">定时检测更新逻辑</param>
    public static void SetTimingDetectAction(Func<IChannel, bool>? timingDetectAction = null)
    {
        _timingDetectAction = timingDetectAction;
    }

    /// <summary>
    /// 设置当前存储模式
    /// </summary>
    /// <param name="sqlLogSwitch">Sql日志开关</param>
    private static void SetCurrentSwitch(bool sqlLogSwitch)
    {
        _switch = sqlLogSwitch;
    }

    /// <summary>
    /// 获取当前存储模式
    /// </summary>
    /// <returns></returns>
    public static bool GetCurrentSwitch()
    {
        return _switch;
    }
}