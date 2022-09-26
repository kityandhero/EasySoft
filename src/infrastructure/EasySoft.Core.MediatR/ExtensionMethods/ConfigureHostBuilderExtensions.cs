using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.MediatR.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceMediatR(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceMediator();
        });

        return builder;
    }

    internal static ConfigureHostBuilder AddAdvanceMediatR(
        this ConfigureHostBuilder builder,
        params Assembly[] assemblies
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceMediator(assemblies);
        });

        return builder;
    }
}