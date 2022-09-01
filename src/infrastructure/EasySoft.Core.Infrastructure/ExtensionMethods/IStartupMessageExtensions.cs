using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class StartupMessageExtensions
{
    public static string BuildMessage(this IStartupMessage startupMessage)
    {
        return new[] { startupMessage.GetMessage(), startupMessage.GetExtra() }
            .ToListFilterNullOrWhiteSpace()
            .Join(" ");
    }

    public static void Print(this ICollection<IStartupMessage> messageCollection)
    {
        messageCollection.ForEach(o =>
        {
            switch (o.GetLevel())
            {
                case LogLevel.Information:
                    LogAssist.Info(o.BuildMessage());
                    break;

                case LogLevel.Warning:
                    LogAssist.Warning(o.BuildMessage());
                    break;

                case LogLevel.Error:
                    LogAssist.Error(o.BuildMessage());
                    break;

                case LogLevel.Critical:
                    LogAssist.Critical(o.BuildMessage());
                    break;

                case LogLevel.Trace:
                    LogAssist.Trace(o.BuildMessage());
                    break;
            }
        });

        messageCollection.Clear();
    }
}