using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceMediatR(
        this ConfigureHostBuilder builder,
        Assembly assembly
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceMediator(assembly);
        });

        return builder;
    }

    internal static ConfigureHostBuilder AddAdvanceMediatR(
        this ConfigureHostBuilder builder,
        IEnumerable<Assembly> assemblies
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceMediator(assemblies);
        });

        return builder;
    }
}