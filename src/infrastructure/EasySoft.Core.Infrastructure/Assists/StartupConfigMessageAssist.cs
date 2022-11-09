using System.Collections.Concurrent;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Assists;

public static class StartupConfigMessageAssist
{
    private static readonly ConcurrentQueue<IStartupMessage> MessageCollection = new();

    static StartupConfigMessageAssist()
    {
        MessageCollection.Enqueue(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(UtilityTools.Standard.ConstCollection.ApplicationStartConfigMessageDivider)
        );

        MessageCollection.Enqueue(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage("Application prepare to start, please wait a moment....")
        );

        MessageCollection.Enqueue(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(UtilityTools.Standard.ConstCollection.ApplicationStartConfigMessageDivider)
        );
    }

    private static void Add(IStartupMessage message)
    {
        MessageCollection.Enqueue(message);
    }

    private static void AddInformation(string message, string extra = "")
    {
        Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(message)
                .SetExtra(extra)
        );
    }

    private static void AddDebug(string message, string extra = "")
    {
        Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(message)
                .SetExtra(extra)
        );
    }

    private static void AddTrace(string message, string extra = "")
    {
        Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(message)
                .SetExtra(extra)
        );
    }

    private static void AddWarning(string message, string extra = "")
    {
        Add(
            new StartupMessage()
                .SetLevel(LogLevel.Warning)
                .SetMessage(message)
                .SetExtra(extra)
        );
    }

    public static void AddConfig(string message, string extra = "")
    {
        AddInformation(message);
    }

    public static void Print()
    {
        var list = MessageCollection.ToListFilterNullable()
            .ToList();

        list.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(
                    UtilityTools.Standard.ConstCollection.ApplicationStartMessageDivider
                )
        );

        list.Print();

        MessageCollection.Clear();
    }
}