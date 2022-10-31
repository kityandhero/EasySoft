using Autofac;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.MultiTenant.ExtensionMethods;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder AddTenantFactory(
        this ContainerBuilder builder,
        Func<HttpContext?, Tenant> tenantBuilder
    )
    {
        builder.Register(c =>
        {
            var httpContext = c.Resolve<IHttpContextAccessor>().HttpContext;

            return tenantBuilder(httpContext);
        }).AsSelf().As<ITenant>().InstancePerLifetimeScope();

        return builder;
    }
}