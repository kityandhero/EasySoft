using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Assists;

public static class StartupDescriptionMessageAssist
{
    private static readonly List<IStartupMessage> MessageCollection = new();

    public static void Add(IStartupMessage message)
    {
        MessageCollection.Add(message);
    }

    public static void Print()
    {
        // ReSharper disable once RedundantAssignment
        // ReSharper disable once EntityNameCapturedOnly.Local
        var message = new StartupMessage();

        var list = MessageCollection.ToListFilterNullable()
            .SortByPropertyValue(SortRule.Desc, nameof(message.Level))
            .ToList();

        list.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    UtilityTools.Standard.ConstCollection.ApplicationStartMessageDescriptionDivider
                )
        );

        list.Print();
    }
}