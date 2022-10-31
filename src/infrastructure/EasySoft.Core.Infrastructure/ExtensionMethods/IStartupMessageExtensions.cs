using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class StartupMessageExtensions
{
    public static string BuildMessage(this IStartupMessage startupMessage)
    {
        return new[] { startupMessage.GetMessage(), startupMessage.GetExtra() }
            .ToListFilterNullOrWhiteSpace()
            .Join(" ");
    }

    public static void Print(this ICollection<IStartupMessage> messageCollection)
    {
        // ReSharper disable once RedundantAssignment
        // ReSharper disable once EntityNameCapturedOnly.Local
        var message = new StartupMessage();

        messageCollection.ToListFilterNullable()
            .SortByPropertyValue(SortRule.Desc, nameof(message.Level))
            .ForEach(o =>
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