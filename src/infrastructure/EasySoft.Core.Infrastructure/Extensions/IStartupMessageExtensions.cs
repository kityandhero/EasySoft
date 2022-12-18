using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.Core.Infrastructure.Extensions;

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
                        LogAssist.Info(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Info(o.GetMessage());
                        LogAssist.Info(o.GetExtra());
                    }

                    break;

                case LogLevel.Warning:
                    if (!newLine)
                    {
                        LogAssist.Warning(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Warning(o.GetMessage());
                        LogAssist.Warning(o.GetExtra());
                    }

                    break;

                case LogLevel.Error:
                    if (!newLine)
                    {
                        LogAssist.Error(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Error(o.GetMessage());
                        LogAssist.Error(o.GetExtra());
                    }

                    break;

                case LogLevel.Critical:
                    if (!newLine)
                    {
                        LogAssist.Critical(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Critical(o.GetMessage());
                        LogAssist.Critical(o.GetExtra());
                    }

                    break;

                case LogLevel.Debug:
                    if (!newLine)
                    {
                        LogAssist.Debug(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Debug(o.GetMessage());
                        LogAssist.Debug(o.GetExtra());
                    }

                    break;

                case LogLevel.Trace:
                    if (!newLine)
                    {
                        LogAssist.Trace(o.BuildMessage());
                    }
                    else
                    {
                        LogAssist.Trace(o.GetMessage());
                        LogAssist.Trace(o.GetExtra());
                    }

                    break;
            }
        });

        messageCollection.Clear();
    }
}