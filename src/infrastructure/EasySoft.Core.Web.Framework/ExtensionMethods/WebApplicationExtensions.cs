using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    /// <summary>
    /// build route areas
    /// </summary>  
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceMapControllers(
        this WebApplication application
    )
    {
        application.UseMvc();

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraEndpointMessageStartDivider
            );

        if (ApplicationConfigurator.GetAllAreas().Any())
        {
            var areaAdjust = ApplicationConfigurator.GetAllAreas()
                .Where(o => !string.IsNullOrWhiteSpace(o.Remove(" ")))
                .ToList();

            if (areaAdjust.Any())
            {
                StartupConfigMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"Areas: {ApplicationConfigurator.GetAllAreas().Join(",")}"
                        )
                );

                application.UseEndpoints(endpoints =>
                {
                    WeaveExtraAction(endpoints, startMessage);

                    areaAdjust.ForEach(o =>
                    {
                        endpoints.MapAreaControllerRoute(
                            name: o,
                            areaName: o,
                            pattern: "{area:exists}/{controller}/{action}"
                        );
                    });

                    endpoints.MapControllerRoute(
                        name: "areaRoute",
                        pattern: "{area:exists}/{controller}/{action}"
                    );

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                });
            }
            else
            {
                application.UseEndpoints(endpoints =>
                {
                    WeaveExtraAction(endpoints, startMessage);

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                });
            }
        }
        else
        {
            application.UseEndpoints(endpoints =>
            {
                WeaveExtraAction(endpoints, startMessage);

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        return application;
    }

    /// <summary>
    /// 织入扩展的 IEndpointRouteBuilder Action
    /// </summary>
    private static void WeaveExtraAction(
        IEndpointRouteBuilder endpoint,
        IStartupMessage startNormalMessageAssist
    )
    {
        var extraActions = ApplicationConfigurator.GetAllEndpointRouteBuilderExtraActions().ToList();

        if (extraActions.Count <= 0)
        {
            return;
        }

        StartupEndPointExtraActionMessageAssist.Add(startNormalMessageAssist);

        for (var i = 0; i < extraActions.Count; i++)
        {
            var extraAction = extraActions[i];

            var action = extraAction.GetAction();

            if (action == null)
            {
                continue;
            }

            var name = extraAction.GetName();

            if (!string.IsNullOrWhiteSpace(name))
            {
                StartupEndPointExtraActionMessageAssist.Add(
                    new StartupMessage()
                        .SetLevel(LogLevel.Information)
                        .SetMessage(
                            $"{i + 1}: {extraAction.GetName()}"
                        )
                );
            }

            action(endpoint);
        }
    }
}