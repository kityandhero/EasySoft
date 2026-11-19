using LogLevel = NLog.LogLevel;

namespace EasySoft.Core.AdvanceNLog.Renderers;

[LayoutRenderer("shortlevel")]
public class ShortLevelLayoutRenderer : LayoutRenderer
{
    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        var level = logEvent.Level;
        if (level == LogLevel.Trace)
        {
            builder.Append("TRAC");
        }
        else if (level == LogLevel.Debug)
        {
            builder.Append("DBUG");
        }
        else if (level == LogLevel.Info)
        {
            builder.Append("INFO");
        }
        else if (level == LogLevel.Warn)
        {
            builder.Append("WARN");
        }
        else if (level == LogLevel.Error)
        {
            builder.Append("ERRO");
        }
        else if (level == LogLevel.Fatal)
        {
            builder.Append("FATL");
        }
        else
        {
            builder.Append("????");
        }
    }
}