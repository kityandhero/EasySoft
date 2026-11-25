using EasySoft.UtilityTools.AspNetCore.Options;
using EasySoft.UtilityTools.Core.Assists;

namespace EasySoft.UtilityTools.AspNetCore.Extensions;
 
/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// UseAdvanceStaticFiles
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        var app = application.UseHostFiltering();

        var isService = app.ApplicationServices.GetService<IServiceProviderIsService>();

        if (isService?.IsService(typeof(AdvanceStaticFileOptions)) != true)
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = app.ApplicationServices.GetService<AdvanceStaticFileOptions>();

            if (staticFileOptions != null)
            {
                application.UseStaticFiles(staticFileOptions);

                FlagAssist.SetAdvanceStaticFileOptionsSwitchOpen();
            }
        }

        return application;
    }
}