using EasySoft.Core.DistributedLock.RedLock.Assist;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.DistributedLock.RedLock.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.DistributedLock.RedLock.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    internal static ConfigureHostBuilder AddAdvanceRedLock(
        this ConfigureHostBuilder builder,
        RedLockOptions options
    )
    {
        builder.ConfigureServices((_, services) =>
            {
                var advanceRedLockFactory = RedLockAssist.GetAdvanceRedLockFactory(options);

                // 对应 .AsSelf() + .PreserveExistingDefaults() + .SingleInstance()
                services.TryAddSingleton(advanceRedLockFactory);

                // 对应 .As<IAdvanceRedLockFactory>() + .PreserveExistingDefaults()
                services.TryAddSingleton<IAdvanceRedLockFactory>(advanceRedLockFactory);
            }
        );

        return builder;
    }
}