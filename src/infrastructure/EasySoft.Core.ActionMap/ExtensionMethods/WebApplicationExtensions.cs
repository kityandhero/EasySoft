using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.ActionMap.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseActionMap(
        this WebApplication application
    )
    {
        if (FlagAssist.GetActionMapSwitch())
        {
            return application;
        }

        ApplicationConfigActionAssist.AddEndpointRouteBuilderAction(endpoints => { endpoints.MapActionMap(); });

        FlagAssist.SetActionMapSwitchOpen();

        StartupMessage.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                $"UseActionMap: enabled{(!FlagAssist.StartupUrls.Any() ? "." : $", you can access {FlagAssist.StartupUrls.Select(o => $"{o}/ActionMap").Join(" ")}")} to visit it."
        });

        return application;
    }
}