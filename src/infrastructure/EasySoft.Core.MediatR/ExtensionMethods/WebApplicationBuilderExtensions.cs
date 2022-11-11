using System.Reflection;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMediatR)}."
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
            $"{nameof(AddAdvanceMediatR)}."
        );

        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}