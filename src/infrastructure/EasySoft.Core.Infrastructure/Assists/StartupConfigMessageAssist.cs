using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// StartupConfigMessageAssist
/// </summary>
public static class StartupConfigMessageAssist
{
    private static readonly IList<IStartupMessage> MessageCollection = new List<IStartupMessage>();

    static StartupConfigMessageAssist()
    {
        MessageCollection.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(ConstCollection.ApplicationStartConfigMessageDivider)
        );

        MessageCollection.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage("Application prepare to start, please wait a moment....")
        );

        MessageCollection.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(ConstCollection.ApplicationStartConfigMessageDivider)
        );
    }

    private static void Add(IStartupMessage message)
    {
        MessageCollection.Add(message);
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

    /// <summary>
    /// AddConfig
    /// </summary>
    /// <param name="message"></param>
    /// <param name="extra"></param>
    public static void AddConfig(string message, string extra = "")
    {
        AddInformation(message, extra);
    }

    /// <summary>
    /// AddTraceDivider
    /// </summary>
    /// <param name="divider"></param>
    public static void AddTraceDivider(string divider = ConstCollection.PromptMessageDivider)
    {
        AddTrace(divider);
    }

    /// <summary>
    /// Print
    /// </summary>
    public static void Print()
    {
        var list = MessageCollection.ToListFilterNullable()
            .ToList();

        list.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Trace)
                .SetMessage(
                    ConstCollection.ApplicationStartMessageDivider
                )
        );

        list.Print();

        MessageCollection.Clear();
    }
}