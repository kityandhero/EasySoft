using EasySoft.Core.Infrastructure.Extensions;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// StartupWarnMessageAssist
/// </summary>
public static class StartupWarnMessageAssist
{
    private static readonly IList<IStartupMessage> MessageCollection = new List<IStartupMessage>();

    private static void Add(IStartupMessage message)
    {
        MessageCollection.Add(message);
    }

    /// <summary>
    /// AddWarning
    /// </summary>
    /// <param name="message"></param>
    /// <param name="extra"></param>
    public static void AddWarning(string message, string extra = "")
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
        var list = MessageCollection.ToListFilterNullable().ToList();

        list.Print();

        MessageCollection.Clear();
    }
}