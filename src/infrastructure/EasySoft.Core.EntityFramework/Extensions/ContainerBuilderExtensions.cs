using EasySoft.Core.EntityFramework.Contexts.ContextFactories;

namespace EasySoft.Core.EntityFramework.Extensions;

/// <summary>
/// ContainerBuilderExtensions
/// </summary>
public static class ContainerBuilderExtensions
{
    /// <summary>
    /// AddAdvanceTenantContextFactory
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TFactory"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ContainerBuilder AddAdvanceTenantContextFactory<TFactory, T>(
        this ContainerBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    {
        builder.RegisterType<TFactory>().AsSelf().As<AdvanceTenantContextFactory<T>>()
            .InstancePerLifetimeScope();

        return builder;
    }

    /// <summary>
    /// AddAdvanceTenantContext
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TFactory"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ContainerBuilder AddAdvanceTenantContext<TFactory, T>(
        this ContainerBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    {
        builder.Register(c =>
        {
            var factory = c.Resolve<TFactory>();

            return factory.CreateDbContext();
        }).AsSelf().As<T>().InstancePerLifetimeScope();

        return builder;
    }
}