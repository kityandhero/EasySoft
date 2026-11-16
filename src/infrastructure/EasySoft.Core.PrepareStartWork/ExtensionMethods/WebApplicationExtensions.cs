using EasySoft.Core.PrepareStartWork.PrepareWorks;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationExtensions
{
    /// <summary>
    /// UsePrepareStartWork
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UsePrepareStartWork)}."
        );

        var app = application.UseHostFiltering();

        #region PrepareCovertStartWork

        if (!app.ApplicationServices.IsRegistered<IPrepareCovertStartWork>())
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "PrepareCovertStartWork not exist, ignore."
            );

            return application;
        }

        var prepareCovertStartWork = app.ApplicationServices.GetRequiredService<IPrepareCovertStartWork>();

        if (prepareCovertStartWork == null)
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "PrepareCovertStartWork is null, ignore."
            );

            return application;
        }

        prepareCovertStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareCovertStartWork do work complete."
        );

        #endregion

        #region PrepareStartWork

        if (!app.ApplicationServices.IsRegistered<IPrepareStartWork>())
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "PrepareStartWork not exist, ignore."
            );

            return application;
        }

        var prepareStartWork = app.ApplicationServices.GetRequiredService<IPrepareStartWork>();

        if (prepareStartWork == null)
        {
            StartupDescriptionMessageAssist.AddPrompt(
                "PrepareStartWork is null, ignore."
            );

            return application;
        }

        prepareStartWork.DoWork();

        StartupDescriptionMessageAssist.AddPrompt(
            "PrepareStartWork do work complete."
        );

        #endregion

        return application;
    }
}