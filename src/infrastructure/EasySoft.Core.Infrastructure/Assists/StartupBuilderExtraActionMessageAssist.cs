using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// StartupBuilderExtraActionMessageAssist
/// </summary>
public static class StartupBuilderExtraActionMessageAssist
{
    private static readonly List<IStartupMessage> MessageCollection = new();

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="message"></param>
    public static void Add(IStartupMessage message)
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
    /// Print
    /// </summary>
    public static void Print()
    {
        MessageCollection.Print();
    }
}