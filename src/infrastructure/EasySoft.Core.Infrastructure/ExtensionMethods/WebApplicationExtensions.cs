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
}