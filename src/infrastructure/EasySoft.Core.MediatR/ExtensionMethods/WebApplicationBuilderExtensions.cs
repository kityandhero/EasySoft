using System.Reflection;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceMediatR)}()."
        );

        builder.Host.AddAdvanceMediatR(assembly);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        IEnumerable<Assembly> assemblies
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAdvanceMediatR)}()."
        );

        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}