using EasySoft.Core.PrepareStartWork.PrepareWorks;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UsePrepareStartWork)}."
        );

        var app = application.UseHostFiltering();

        var prepareCovertStartWork = app.ApplicationServices.GetRequiredService<IPrepareCovertStartWork>();

        prepareCovertStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareCovertStartWork do work complete."
        );

        var isService = app.ApplicationServices.GetService<IServiceProviderIsService>();

        if (isService?.IsService(typeof(IPrepareStartWork)) != true)
        {
            return application;
        }

        var prepareStartWork = app.ApplicationServices.GetRequiredService<IPrepareStartWork>();

        prepareStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareStartWork do work complete."
        );

        return application;
    }
}