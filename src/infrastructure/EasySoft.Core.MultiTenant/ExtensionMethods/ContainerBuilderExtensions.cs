namespace EasySoft.Core.MultiTenant.ExtensionMethods;

public static class ContainerBuilderExtensions
{
    public static IServiceCollection AddTenantFactory(
        this IServiceCollection services,
        Func<HttpContext, Tenant> tenantBuilder
    )
    {
        // 等价于 .AsSelf()：Tenant 自身可解析
        services.AddScoped<Tenant>(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return tenantBuilder(httpContext);
            }
        );

        // 等价于 .As<ITenant>()：接口解析复用同一个 Tenant 实例
        services.AddScoped<ITenant>(sp => sp.GetRequiredService<Tenant>());

        return services;
    }
}