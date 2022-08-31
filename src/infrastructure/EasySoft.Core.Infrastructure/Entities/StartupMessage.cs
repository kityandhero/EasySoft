using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Entities;

public class StartupMessage
{
    public LogLevel LogLevel { get; set; }

    public string Message { get; set; }

    public string Extra { get; set; }

    public StartupMessage()
    {
        LogLevel = LogLevel.Information;
        Message = "";
        Extra = "";
    }

    private static readonly List<StartupMessage> StartupMessageCollection;

    static StartupMessage()
    {
        StartupMessageCollection = new List<StartupMessage>
        {
            new()
            {
                LogLevel = LogLevel.Information,
                Message = UtilityTools.Standard.ConstCollection.ApplicationStartBeginDivider,
            },
            new()
            {
                LogLevel = LogLevel.Information,
                Message = "Application prepare to start, please wait a moment...."
            }
        };
    }

    public static void Add(StartupMessage startupMessage)
    {
        StartupMessageCollection.Add(startupMessage);
    }

    private static string BuildAllMessage(StartupMessage o)
    {
        return new[] { o.Message, o.Extra }.ToListFilterNullOrWhiteSpace().Join(" ");
    }

    public static void Print()
    {
        StartupMessageCollection.ForEach(o =>
        {
            switch (o.LogLevel)
            {
                case LogLevel.Information:
                    LogAssist.Info(BuildAllMessage(o));
                    break;

                case LogLevel.Warning:
                    LogAssist.Warning(BuildAllMessage(o));
                    break;

                case LogLevel.Error:
                    LogAssist.Error(BuildAllMessage(o));
                    break;

                case LogLevel.Critical:
                    LogAssist.Critical(BuildAllMessage(o));
                    break;

                case LogLevel.Trace:
                    LogAssist.Trace(BuildAllMessage(o));
                    break;
            }
        });

        StartupMessageCollection.Clear();
    }
}