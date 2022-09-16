using Autofac;
using EasySoft.Core.DistributedLock.RedLock.Assist;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.DistributedLock.RedLock.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.DistributedLock.RedLock.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceRedLock(this ConfigureHostBuilder builder, RedLockOptions options)
    {
        builder.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
        {
            var factory = RedLockAssist.GetAdvanceRedLockFactory(options);

            containerBuilder.RegisterInstance(factory).AsSelf().As<IAdvanceRedLockFactory>().PreserveExistingDefaults()
                .SingleInstance();
        });

        return builder;
    }
}