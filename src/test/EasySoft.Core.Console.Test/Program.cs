using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;

var serviceProvider = ConsoleServiceProviderAssist.CreateServiceProvider((serviceCollection, configuration) =>
    {
        Console.WriteLine("-------ConsoleServiceProviderAssist-------");

        serviceCollection.AddCacheCore(configuration);
        serviceCollection.AddAdvanceCacheImplementation();
    }
);