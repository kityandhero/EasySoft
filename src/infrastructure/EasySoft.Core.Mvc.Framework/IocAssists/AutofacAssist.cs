using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using EasySoft.Core.Mvc.Framework.CommonAssists;
using EasySoft.UtilityTools.Assists;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Mvc.Framework.IocAssists;

public class AutofacAssist
{
    private const string MerchantAutoFacConfigFile = "autoFac.json";

    public static readonly AutofacAssist Instance = new();
    public ILifetimeScope Container { get; set; } = null!;

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

    internal static void Init(ContainerBuilder builder)
    {
        builder.RegisterModule(ConfigurationModule);
    }

    public T Resolve<T>() where T : notnull
    {
        return Container.Resolve<T>();
    }

    public T ResolveKeyed<T>(string serviceKey) where T : notnull
    {
        return Container.ResolveKeyed<T>(serviceKey);
    }

    public T ResolveKeyed<T>(string serviceKey, params Parameter[] parameters) where T : notnull
    {
        return Container.ResolveKeyed<T>(serviceKey,
            parameters
        );
    }

    public object Resolve(Type serviceType)
    {
        return Container.Resolve(serviceType);
    }

    public object ResolveKeyed(string serviceKey, Type serviceType)
    {
        return Container.ResolveKeyed(serviceKey, serviceType);
    }

    public bool IsRegistered<T>() where T : notnull
    {
        return Container.IsRegistered<T>();
    }

    public bool IsRegisteredWithKey<T>(string serviceKey) where T : notnull
    {
        return Container.IsRegisteredWithKey<T>(serviceKey);
    }

    public bool IsRegistered(Type serviceType)
    {
        return Container.IsRegistered(serviceType);
    }

    public bool IsRegisteredWithKey(string serviceKey, Type serviceType)
    {
        return Container.IsRegisteredWithKey(serviceKey, serviceType);
    }
}