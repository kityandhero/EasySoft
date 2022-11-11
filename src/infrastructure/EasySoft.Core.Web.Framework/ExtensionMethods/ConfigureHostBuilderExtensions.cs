using Autofac;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceDefaultApplicationChannel(
        this ConfigureHostBuilder builder
    )
    {
        builder.AddAdvanceApplicationChannel(
            ApplicationChannel.DefaultChannel,
            ApplicationChannel.DefaultName
        );

        return builder;
    }

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

        StartupDescriptionMessageAssist.AddPrompt(
            $"ApplicationChannel name is {name}, identification is {channel}."
        );

        return builder;
    }
}