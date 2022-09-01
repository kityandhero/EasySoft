using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.PrepareStartWork.PrepareWorks;
using Microsoft.AspNetCore.Builder;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UsePrepareStartWork(
        this WebApplication application
    )
    {
        var prepareCovertStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareCovertStartWork>();

        prepareCovertStartWork.DoWork();

        StartupNormalMessageAssist.Add(
            new StartupMessage().SetLevel(LogLevel.Information).SetMessage(
                "PrepareCovertStartWork do work complete."
            )
        );

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
        {
            return application;
        }

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        StartupNormalMessageAssist.Add(
            new StartupMessage().SetLevel(LogLevel.Information).SetMessage(
                "PrepareStartWork do work complete."
            )
        );

        return application;
    }
}