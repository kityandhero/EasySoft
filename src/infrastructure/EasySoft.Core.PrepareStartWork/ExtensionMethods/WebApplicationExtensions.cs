using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.PrepareStartWork.PrepareWorks;
using Microsoft.AspNetCore.Builder;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

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