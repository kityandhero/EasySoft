using System.ComponentModel;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.AutoFac.IocAssists;

[Description("该类暂时未考虑用处")]
public static class AutoFacConsoleAssist
{
    public static IServiceProvider CreateServiceProvider(
        Action<IServiceCollection> actionServiceCollection,
        Action<ContainerBuilder> actionContainerBuilder
    )
    {
        var serviceProviderFactory = new AutofacServiceProviderFactory();

        IServiceCollection services = new ServiceCollection();

        actionServiceCollection(services);

        var containerBuilder = serviceProviderFactory.CreateBuilder(services);

        actionContainerBuilder(containerBuilder);

        var serviceProvider = serviceProviderFactory.CreateServiceProvider(containerBuilder);

        AutofacAssist.Instance.SetContainer(serviceProvider.GetAutofacRoot());

        return serviceProvider;
    }
}