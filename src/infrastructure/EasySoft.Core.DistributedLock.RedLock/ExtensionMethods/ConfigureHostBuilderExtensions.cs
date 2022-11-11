using EasySoft.Core.DistributedLock.RedLock.Assist;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.DistributedLock.RedLock.Interfaces;

namespace EasySoft.Core.DistributedLock.RedLock.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceRedLock(this ConfigureHostBuilder builder, RedLockOptions options)
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            var advanceRedLockFactory = RedLockAssist.GetAdvanceRedLockFactory(options);

            containerBuilder.RegisterInstance(advanceRedLockFactory).AsSelf().As<IAdvanceRedLockFactory>()
                .PreserveExistingDefaults()
                .SingleInstance();
        });

        return builder;
    }
}