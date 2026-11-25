using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.AdvanceNLog.Renderers;

[LayoutRenderer("ansi-highlight")]
public class AnsiHighlightLayoutRenderer : LayoutRenderer
{
    public Layout Inner { get; set; } = null!;

    private static readonly Regex LineSplitRegex = new(
        @"^(\[\d{4}\-\d{2}\-\d{2} \d{2}:\d{2}:\d{2}\.\d{4}\])\|(TRAC|DBUG|INFO|WARN|ERRO|FATL) => (.*)$",
        RegexOptions.Compiled
    );

    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        if (Inner is null)
        {
            throw new NLogConfigurationException("ansi-highlight: Inner must be assigned.");
        }

        var raw = Inner.Render(logEvent);

        // 只针对 Microsoft.Hosting.Lifetime 做硬编码占位符替换，其余日志不动
        if (logEvent.LoggerName == "Microsoft.Hosting.Lifetime")
        {
            if (raw.Contains("Hosting environment:"))
            {
                raw = raw.Replace(
                    "Hosting environment:  ",
                    "Hosting environment: " + HostEnvironmentAssist.GetEnvironment().EnvironmentName
                );
            }

            if (raw.Contains("Content root path:"))
            {
                raw = raw.Replace(
                    "Content root path:  ",
                    "Content root path: " + HostEnvironmentAssist.GetEnvironment().ContentRootPath
                );
            }
        }

        var ansiEnabled = !Console.IsOutputRedirected;

        if (!ansiEnabled)
        {
            builder.Append(raw);
            return;
        }

        var match = LineSplitRegex.Match(raw);

        if (match.Success)
        {
            var datePart = match.Groups[1].Value;
            var levelText = match.Groups[2].Value;
            var msgPart = match.Groups[3].Value;

            var levelColor = levelText switch
            {
                "TRAC" => "\x1b[90m",
                "DBUG" => "\x1b[36m",
                "INFO" => "\x1b[32m",
                "WARN" => "\x1b[33m",
                "ERRO" => "\x1b[31m",
                "FATL" => "\x1b[31;1m",
                _ => "\x1b[0m"
            };

            const string gray = "\x1b[90m";
            const string reset = "\x1b[0m";

            // 1. 时间：灰色
            builder.Append($"{gray}{datePart}|{reset}");

            // 2. 级别标识：级别色
            builder.Append($"{levelColor}{levelText}{reset}");

            // 3. 箭头：灰色
            builder.Append($"{gray} => {reset}");

            // 消息体：外层先开启级别色，标记只临时切灰
            builder.Append(levelColor);

            var decoratedMsg = ApplyTagGray(msgPart, levelColor);

            builder.Append(decoratedMsg);
            builder.Append(reset);
        }
        else
        {
            builder.Append(raw);
        }
    }

    /// <summary>
    /// 把 [DESC] [EXEC] [HINT] 标签渲染灰色，标签结束切回传入的级别色
    /// </summary>
    private static string ApplyTagGray(string input, string levelColorCode)
    {
        // 匹配 [DESC] / [EXEC] / [HINT]
        return Regex.Replace(
            input,
            @"\[(DESC|EXEC|HINT|CONF)\]",
            $"\x1b[90m[$1]{levelColorCode}"
        );
    }
}