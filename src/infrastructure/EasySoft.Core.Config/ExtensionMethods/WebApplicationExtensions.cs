using EasySoft.Core.Config.Options;

namespace EasySoft.Core.Config.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceStaticFiles(
        this WebApplication application
    )
    {
        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .IsRegistered<AdvanceStaticFileOptions>())
        {
            application.UseStaticFiles();
        }
        else
        {
            var staticFileOptions = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
                .Resolve<AdvanceStaticFileOptions>();

            application.UseStaticFiles(staticFileOptions);

            FlagAssist.SetAdvanceStaticFileOptionsSwitchOpen();
        }

        return application;
    }
}