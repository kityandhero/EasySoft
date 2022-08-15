using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Mvc.Framework.PrepareWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using EasySoft.UtilityTools.ExtensionMethods;
using Hangfire;

namespace EasySoft.Core.Mvc.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetEnable())
        {
            application.RecordInformation(
                "hangfire: disable."
            );

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        application.RecordInformation(
            "hangfire: enable, access:https://[host]:[port]/hangfire."
        );

        return application;
    }

    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            application.RecordInformation(
                "swagger: disable."
            );

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        application.RecordInformation(
            "swagger: enable, access https://[host]:[port]/swagger/index.html."
        );

        return application;
    }

    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application,
        string root = "wwwroot",
        string path = "/"
    )
    {
        var provider = new FileExtensionContentTypeProvider
        {
            Mappings =
            {
                [".properties"] = "application/octet-stream"
            }
        };

        application.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    root,
                    path
                )
            ),
            RequestPath = "/Content",
            ContentTypeProvider = provider
        });

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

        application.UseMvc();

        return application;
    }

    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        var prepareCovertStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareCovertStartWork>();

        prepareCovertStartWork.DoWork();

        application.RecordInformation(
            "prepareCovertStartWork do work complete."
        );

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
        {
            return application;
        }

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        application.RecordInformation(
            "prepareStartWork do work complete."
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
}