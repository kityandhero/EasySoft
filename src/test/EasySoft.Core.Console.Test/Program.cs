using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.DependencyInjection.IocAssists;

var serviceProvider = ConsoleServiceProviderAssist.CreateServiceProvider((serviceCollection, configuration) =>
    {
        Console.WriteLine("-------ConsoleServiceProviderAssist-------");

        serviceCollection.AddCacheCore();
        serviceCollection.AddAdvanceCacheImplementation();
    }
);