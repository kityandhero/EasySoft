using EasySoft.Core.AdvanceNLog.ExtensionMethods;
using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Channels;

namespace EasySoft.Core.Console.Kernel.Assists;

/// <summary>
/// ConsoleBuilderAssist
/// </summary>
public static class ConsoleBuilderAssist
{
    /// <summary>
    /// CreateServiceProvider
    /// </summary>
    /// <param name="applicationChannel"></param>
    /// <param name="whetherDevelop">是否开发环境</param>
    /// <returns></returns>
    public static ServiceProvider CreateServiceProvider(  
        IApplicationChannel applicationChannel,
        bool whetherDevelop = false
    )
    {
        var serviceProvider = ConsoleServiceProviderAssist.CreateServiceProvider((serviceCollection, configuration) =>
            {
                serviceCollection.AddAdvanceNLog(
                    configuration,
                    whetherDevelop
                );

                serviceCollection.AddCacheCore(configuration);

                serviceCollection.AddAdvanceCacheImplementation();

                serviceCollection.AddSingleton(applicationChannel);
            }
        );

        return serviceProvider;
    }
}