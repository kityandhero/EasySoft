using Autofac;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceApplicationChannel(
        this ConfigureHostBuilder builder,
        int channel,
        string name
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterInstance(new ApplicationChannel().SetChannel(channel).SetName(name))
                .As<IApplicationChannel>().SingleInstance();
        });

        return builder;
    }

    internal static ConfigureHostBuilder AddAdvanceDefaultApplicationChannel(
        this ConfigureHostBuilder builder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<IApplicationChannel>().As<IApplicationChannel>().SingleInstance();
        });

        return builder;
    }
}