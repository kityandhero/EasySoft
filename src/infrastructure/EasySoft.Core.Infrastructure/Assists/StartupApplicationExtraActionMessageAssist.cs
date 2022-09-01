using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Infrastructure.Assists;

public static class StartupApplicationExtraActionMessageAssist
{
    private static readonly List<IStartupMessage> MessageCollection = new();

    public static void Add(IStartupMessage message)
    {
        MessageCollection.Add(message);
    }

    public static void Print()
    {
        MessageCollection.Print();
    }
}