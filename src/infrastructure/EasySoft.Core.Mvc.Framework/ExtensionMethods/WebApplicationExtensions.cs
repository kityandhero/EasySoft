using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Mvc.Framework.ConfigAssist;
using EasySoft.Core.Mvc.Framework.IocAssists;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.Mvc.Framework.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        return SwaggerConfigAssist.SetSwagger(application);
    }

    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        return HangfireConfigAssist.SetHangfire(application);
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

        if (SwaggerConfigAssist.GetEnable())
        {
            application.RecordInformation(
                "swagger: enable, access https://[host]:[port]/swagger/index.html."
            );
        }
        else
        {
            application.RecordInformation(
                "swagger: disable."
            );
        }

        if (HangfireConfigAssist.GetEnable())
        {
            application.RecordInformation(
                "hangfire: enable, access:https://[host]:[port]/hangfire."
            );
        }
        else
        {
            application.RecordInformation(
                "hangfire: disable."
            );
        }

        application.RecordInformation(
            "if you use autoFac, you can set the autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        );

        application.RecordInformation(
            "you can get all controller actions by visit https://[host]:[port]/[controller]/getAllActions where controller inherited from CustomControllerBase."
        );

        AutofacAssist.Instance.Container = application.UseHostFiltering().ApplicationServices.GetAutofacRoot();

        return application;
    }

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
}