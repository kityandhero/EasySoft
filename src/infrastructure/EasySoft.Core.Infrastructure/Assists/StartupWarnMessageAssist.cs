using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Infrastructure.Assists;

public class StartupWarnMessageAssist
{
    private static readonly IList<IStartupMessage> MessageCollection = new List<IStartupMessage>();

    private static void Add(IStartupMessage message)
    {
        MessageCollection.Add(message);
    }

    public static void AddWarning(string message, string extra = "")
    {
        Add(
            new StartupMessage()
                .SetLevel(LogLevel.Warning)
                .SetMessage(message)
                .SetExtra(extra)
        );
    }

    public static void Print()
    {
        var list = MessageCollection.ToListFilterNullable().ToList();

        list.Print();

        MessageCollection.Clear();
    }
}