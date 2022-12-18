using EasySoft.Core.Infrastructure.Extensions;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// StartupApplicationExtraActionMessageAssist
/// </summary>
public static class StartupApplicationExtraActionMessageAssist
{
    private static readonly IList<IStartupMessage> MessageCollection = new List<IStartupMessage>();

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
    /// AddTraceDivider
    /// </summary>
    /// <param name="divider"></param>
    public static void AddTraceDivider(string divider = ConstCollection.PromptMessageDivider)
    {
        AddTrace(divider);
    }

    /// <summary>
    /// AddPrompt
    /// </summary>
    /// <param name="message"></param>
    /// <param name="extra"></param>
    public static void AddPrompt(string message, string extra = "")
    {
        AddTrace($"DESC: {message}", extra);
    }

    /// <summary>
    /// AddExecute
    /// </summary>
    /// <param name="message"></param>
    /// <param name="extra"></param>
    /// <param name="supplementRoundBracket"></param>
    public static void AddExecute(string message, string extra = "", bool supplementRoundBracket = false)
    {
        AddTraceDivider();

        AddTrace($"EXEC: {message}{(supplementRoundBracket ? "()" : "")}", extra);
    }

    /// <summary>
    /// Print
    /// </summary>
    public static void Print()
    {
        MessageCollection.Print();

        MessageCollection.Clear();
    }
}