using EasySoft.Core.Infrastructure.Assists;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Entities;

public class StartupMessage
{
    public LogLevel LogLevel { get; set; }

    public string Message { get; set; }

    public StartupMessage()
    {
        LogLevel = LogLevel.Information;
        Message = "";
    }

    public static readonly List<StartupMessage> StartupMessageCollection;

    static StartupMessage()
    {
        StartupMessageCollection = new List<StartupMessage>();
    }

    public static void Print()
    {
        StartupMessageCollection.ForEach(o =>
        {
            switch (o.LogLevel)
            {
                case LogLevel.Information:
                    LogAssist.Info(o.Message);
                    break;

                case LogLevel.Warning:
                    LogAssist.Warning(o.Message);
                    break;

                case LogLevel.Error:
                    LogAssist.Error(o.Message);
                    break;

                case LogLevel.Critical:
                    LogAssist.Critical(o.Message);
                    break;

                case LogLevel.Trace:
                    LogAssist.Trace(o.Message);
                    break;

                default:
                    break;
            }
        });

        StartupMessageCollection.Clear();
    }
}