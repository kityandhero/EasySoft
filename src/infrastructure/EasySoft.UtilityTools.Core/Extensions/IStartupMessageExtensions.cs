using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Startup;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// StartupMessageExtensions
/// </summary>
public static class StartupMessageExtensions
{
    /// <summary>
    /// BuildMessage
    /// </summary>
    /// <param name="startupMessage"></param>
    /// <returns></returns>
    public static string BuildMessage(this IStartupMessage startupMessage)
    {
        return new[] { startupMessage.GetMessage(), startupMessage.GetExtra() }
            .ToListFilterNullOrWhiteSpace()
            .Join(" ");
    }

    /// <summary>
    /// Print
    /// </summary>
    /// <param name="messageCollection"></param>
    public static void Print(this ICollection<IStartupMessage> messageCollection)
    {
        messageCollection.ForEach(o =>
            {
                var newLine = o.GetExtraNewLie();

                switch (o.Level)
                {
                    case LogLevel.Information:
                        if (!newLine)
                        {
                            LoggerAssist.Info(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Info(o.GetMessage());
                            LoggerAssist.Info(o.GetExtra());
                        }

                        break;

                    case LogLevel.Warning:
                        if (!newLine)
                        {
                            LoggerAssist.Warning(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Warning(o.GetMessage());
                            LoggerAssist.Warning(o.GetExtra());
                        }

                        break;

                    case LogLevel.Error:
                        if (!newLine)
                        {
                            LoggerAssist.Error(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Error(o.GetMessage());
                            LoggerAssist.Error(o.GetExtra());
                        }

                        break;

                    case LogLevel.Critical:
                        if (!newLine)
                        {
                            LoggerAssist.Critical(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Critical(o.GetMessage());
                            LoggerAssist.Critical(o.GetExtra());
                        }

                        break;

                    case LogLevel.Debug:
                        if (!newLine)
                        {
                            LoggerAssist.Debug(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Debug(o.GetMessage());
                            LoggerAssist.Debug(o.GetExtra());
                        }

                        break;

                    case LogLevel.Trace:
                        if (!newLine)
                        {
                            LoggerAssist.Trace(o.BuildMessage());
                        }
                        else
                        {
                            LoggerAssist.Trace(o.GetMessage());
                            LoggerAssist.Trace(o.GetExtra());
                        }

                        break;
                }
            }
        );

        messageCollection.Clear();
    }
}