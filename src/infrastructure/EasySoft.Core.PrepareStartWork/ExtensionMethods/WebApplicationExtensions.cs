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

        var prepareCovertStartWork = application
            .UseHostFiltering()
            .ApplicationServices
            .GetAutofacRoot()
            .Resolve<IPrepareCovertStartWork>();

        prepareCovertStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareCovertStartWork do work complete."
        );

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
            return application;

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareStartWork do work complete."
        );

        return application;
    }
}