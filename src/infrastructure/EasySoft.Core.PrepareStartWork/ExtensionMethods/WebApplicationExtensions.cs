using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.PrepareStartWork.PrepareWorks;
using Microsoft.AspNetCore.Builder;
using EasySoft.Core.Infrastructure.ExtensionMethods;

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

        application.RecordInformation("prepareCovertStartWork do work complete.");

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
        {
            return application;
        }

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        application.RecordInformation("prepareStartWork do work complete.");

        return application;
    }
}