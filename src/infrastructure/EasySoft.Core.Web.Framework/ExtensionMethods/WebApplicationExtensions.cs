using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    /// <summary>
    /// build route areas
    /// </summary>
    /// <param name="application"></param>
    /// <param name="areas"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceMapControllers(
        this WebApplication application,
        List<string> areas
    )
    {
        application.UseMvc();

        if (areas.Any())
        {
            var areaAdjust = areas.Where(o => !string.IsNullOrWhiteSpace(o.Remove(" "))).ToList();

            if (areaAdjust.Any())
            {
                application.UseEndpoints(endpoints =>
                {
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        return application;
    }
}