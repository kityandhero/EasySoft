using EasySoft.Core.Infrastructure.Entities;
using EasySoft.Core.Web.Framework.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
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

        if (ApplicationConfigActionAssist.GetAreaCollection().Any())
        {
            var areaAdjust = ApplicationConfigActionAssist.GetAreaCollection()
                .Where(o => !string.IsNullOrWhiteSpace(o.Remove(" ")))
                .ToList();

            if (areaAdjust.Any())
            {
                StartupMessage.StartupMessageCollection.Add(new StartupMessage
                {
                    LogLevel = LogLevel.Information,
                    Message = $"Areas: {ApplicationConfigActionAssist.GetAreaCollection().Join(",")}"
                });

                application.UseEndpoints(endpoints =>
                {
                    ApplicationConfigActionAssist.GetEndpointRouteBuilderActionCollection()
                        .ForEach(action => { action(endpoints); });

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
                    ApplicationConfigActionAssist.GetEndpointRouteBuilderActionCollection()
                        .ForEach(action => { action(endpoints); });

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
                ApplicationConfigActionAssist.GetEndpointRouteBuilderActionCollection()
                    .ForEach(action => { action(endpoints); });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        return application;
    }
}