using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        builder.Host.AddAdvanceMediatR(assembly);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceMediatR(
        this WebApplicationBuilder builder,
        IEnumerable<Assembly> assemblies
    )
    {
        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}