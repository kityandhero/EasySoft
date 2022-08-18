using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Web.Framework.PrepareWorks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetEnable())
        {
            RecordInformation(application, "hangfire: disable."
            );

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        RecordInformation(application, "hangfire: enable, access:https://[host]:[port]/hangfire."
        );

        return application;
    }

    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            RecordInformation(application, "swagger: disable."
            );

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        RecordInformation(application, "swagger: enable, access https://[host]:[port]/swagger/index.html."
        );

        return application;
    }

    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<StaticFileOptions>())
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .Resolve<StaticFileOptions>();

            application.UseStaticFiles(staticFileOptions);
        }

        return application;
    }

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

    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        var prepareCovertStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareCovertStartWork>();

        prepareCovertStartWork.DoWork();

        RecordInformation(application, "prepareCovertStartWork do work complete."
        );

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
        {
            return application;
        }

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        RecordInformation(application, "prepareStartWork do work complete."
        );

        return application;
    }

    internal static WebApplication RecordInformation(
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

    internal static WebApplication RecordWarning(
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