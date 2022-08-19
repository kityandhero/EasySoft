using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication RecordInformation(
        this WebApplication application,
        string log
    )
    {
        application.Logger.Log(
            LogLevel.Information,
            0,
            log,
            null,
            (info, _) => info
        );

        return application;
    }

    public static WebApplication RecordWarning(
        this WebApplication application,
        string log
    )
    {
        application.Logger.Log(
            LogLevel.Warning,
            0,
            log,
            null,
            (info, _) => info
        );

        return application;
    }
}