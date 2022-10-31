using System.Collections.Concurrent;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Assists;

public static class StartupDescriptionMessageAssist
{
    private static readonly ConcurrentQueue<IStartupMessage> MessageCollection = new();

    public static void Add(IStartupMessage message)
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

    public static void AddDescription(string message, string extra = "")
    {
        AddInformation(message, extra);
    }

    public static void AddExecute(string message, string extra = "")
    {
        // AddDebug(message, extra);
    }

    public static void Print()
    {
        var list = MessageCollection.ToListFilterNullable()
            .ToList();

        // list.Add(
        //     new StartupMessage()
        //         .SetLevel(LogLevel.Information)
        //         .SetMessage(
        //             UtilityTools.Standard.ConstCollection.ApplicationStartMessageDescriptionDivider
        //         )
        // );

        list.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(
                    "StartupMessage Complete"
                )
        );

        list.Print();

        MessageCollection.Clear();
    }
}