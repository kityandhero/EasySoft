using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediator(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        builder.Host.AddAdvanceMediatR(assembly);

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceMediator(
        this WebApplicationBuilder builder,
        IEnumerable<Assembly> assemblies
    )
    {
        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}