using Autofac;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.EntityFramework.Contexts.ContextFactories;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder AddAdvanceTenantContextFactory<TFactory, T>(
        this ContainerBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : AdvanceTenantContextBase
    {
        builder.RegisterType<TFactory>().AsSelf().As<AdvanceTenantContextFactory<T>>()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static ContainerBuilder AddAdvanceTenantContext<TFactory, T>(
        this ContainerBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : AdvanceTenantContextBase
    {
        builder.Register(c =>
        {
            var factory = c.Resolve<TFactory>();

            return factory.CreateDbContext();
        }).AsSelf().As<T>().InstancePerLifetimeScope();

        return builder;
    }
}