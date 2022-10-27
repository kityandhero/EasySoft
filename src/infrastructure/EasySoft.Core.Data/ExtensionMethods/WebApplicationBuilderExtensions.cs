using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Data.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddServicesWithInterceptors(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies
    )
    {
        builder.Services.AddAssemblyInterceptors(assemblies);

        return builder;
    }

    public static WebApplicationBuilder AddServicesWithInterceptors(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        builder.Services.AddAssemblyInterceptors(assembly);

        return builder;
    }
}