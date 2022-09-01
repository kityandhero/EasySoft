using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
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

        ApplicationConfigActionAssist.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("MapActionMap")
                .SetAction(endpoints => { endpoints.MapActionMap(); })
        );

        FlagAssist.SetActionMapSwitchOpen();

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"UseActionMap: enabled."
                )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"you can access {FlagAssist.StartupUrls.Select(o => $"{o}/ActionMap").Join(" ")} to get all action info."
                )
        );

        return application;
    }
}