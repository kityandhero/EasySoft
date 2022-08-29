using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.AutoFac.IocAssists;

public class AutofacAssist
{
    private const string MerchantAutoFacConfigFile = "autoFac.json";

    public static readonly AutofacAssist Instance = new();

    private ILifetimeScope? _container;

    private static readonly ConfigurationModule ConfigurationModule;

    static AutofacAssist()
    {
        var config = new ConfigurationBuilder();

        var directory = $"{AppContextAssist.GetBaseDirectory()}/configures/";

        var filePath = $"{directory}{MerchantAutoFacConfigFile}";

        config.AddJsonFile(
            filePath,
            optional: true,
            true
        );

        ConfigurationModule = new ConfigurationModule(config.Build());
    }

    public static void Init(HostBuilderContext hostBuilderContext, ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule(ConfigurationModule);
    }

    public void SetContainer(ILifetimeScope container)
    {
        if (_container != null)
        {
            throw new Exception("container has been set, it disallow set more than once.");
        }

        _container = container;
    }

    public ILifetimeScope GetContainer()
    {
        if (_container == null)
        {
            throw new Exception("Container is null, please assign a value before use it.");
        }

        return _container;
    }

    public T Resolve<T>() where T : notnull
    {
        return GetContainer().Resolve<T>();
    }

    public T ResolveKeyed<T>(string serviceKey) where T : notnull
    {
        return GetContainer().ResolveKeyed<T>(serviceKey);
    }

    public T ResolveKeyed<T>(string serviceKey, params Parameter[] parameters) where T : notnull
    {
        return GetContainer().ResolveKeyed<T>(serviceKey, parameters);
    }

    public object Resolve(Type serviceType)
    {
        return GetContainer().Resolve(serviceType);
    }

    public object ResolveKeyed(string serviceKey, Type serviceType)
    {
        return GetContainer().ResolveKeyed(serviceKey, serviceType);
    }

    public bool IsRegistered<T>() where T : notnull
    {
        return GetContainer().IsRegistered<T>();
    }

    public bool IsRegisteredWithKey<T>(string serviceKey) where T : notnull
    {
        return GetContainer().IsRegisteredWithKey<T>(serviceKey);
    }

    public bool IsRegistered(Type serviceType)
    {
        return GetContainer().IsRegistered(serviceType);
    }

    public bool IsRegisteredWithKey(string serviceKey, Type serviceType)
    {
        return GetContainer().IsRegisteredWithKey(serviceKey, serviceType);
    }
}