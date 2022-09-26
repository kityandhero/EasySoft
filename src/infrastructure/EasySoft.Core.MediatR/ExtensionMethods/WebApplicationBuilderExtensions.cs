using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMediator(this WebApplicationBuilder builder)
    {
        builder.Host.AddAdvanceMediatR();

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceMediator(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies
    )
    {
        builder.Host.AddAdvanceMediatR(assemblies);

        return builder;
    }
}