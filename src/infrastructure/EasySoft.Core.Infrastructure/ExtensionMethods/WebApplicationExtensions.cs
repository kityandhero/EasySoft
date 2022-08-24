using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class WebApplicationExtensions
{
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

    public static WebApplication RecordInformation(
        this WebApplication application,
        ICollection<string> logs
    )
    {
        if (!logs.Any())
        {
            return application;
        }

        logs.ForEach(o =>
        {
            application.Logger.Log(
                LogLevel.Information,
                0,
                o,
                null,
                (info, _) => info
            );
        });

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