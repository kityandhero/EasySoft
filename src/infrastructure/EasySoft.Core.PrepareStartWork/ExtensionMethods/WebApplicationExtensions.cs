using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.Infrastructure.Assists;
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

        LogAssist.Info("prepareCovertStartWork do work complete.");

        if (!application.UseHostFiltering().ApplicationServices.GetAutofacRoot().IsRegistered<IPrepareStartWork>())
        {
            return application;
        }

        var prepareStartWork = application.UseHostFiltering().ApplicationServices.GetAutofacRoot()
            .Resolve<IPrepareStartWork>();

        prepareStartWork.DoWork();

        LogAssist.Info("prepareStartWork do work complete.");

        return application;
    }
}