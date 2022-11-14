using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard;

namespace EasySoft.Core.Infrastructure.Assists;

public static class StartupApplicationExtraActionMessageAssist
{
    private static readonly IList<IStartupMessage> MessageCollection = new List<IStartupMessage>();

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

    public static void AddTraceDivider(string divider = ConstCollection.PromptMessageDivider)
    {
        AddTrace(divider);
    }

    public static void AddPrompt(string message, string extra = "")
    {
        AddTrace($"DESC: {message}", extra);
    }

    public static void AddExecute(string message, string extra = "", bool supplementRoundBracket = false)
    {
        AddTraceDivider();

        AddTrace($"EXEC: {message}{(supplementRoundBracket ? "()" : "")}", extra);
    }

    public static void Print()
    {
        MessageCollection.Print();

        MessageCollection.Clear();
    }
}