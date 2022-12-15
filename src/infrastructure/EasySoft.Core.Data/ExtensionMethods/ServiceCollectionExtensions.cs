using EasySoft.Core.Data.Interceptors;

namespace EasySoft.Core.Data.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    private static List<Type> DefaultInterceptorTypes => new() { typeof(UnitOfWorkInterceptor) };

    public static IServiceCollection AddAdvanceUnitOfWorkInterceptor(
        this IServiceCollection services
    )
    {
        services.TryAddScoped<UnitOfWorkInterceptor>();
        services.TryAddScoped<UnitOfWorkAsyncInterceptor>();

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceInterfaces(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        if (assemblies.Length <= 0) return services;

        assemblies.ForEach(assembly =>
            AddAssemblyBusinessServiceInterfaces(services, assembly)
        );

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceInterfaces(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var targetType = typeof(IBusinessService);

        var serviceTypes = assembly.GetExportedTypes()
            .Where(type => type.IsInterface && type.IsAssignableTo(targetType))
            .ToList();

        BusinessServiceAssist.AddRange(serviceTypes);

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceImplementations(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        if (assemblies.Length <= 0) return services;

        assemblies.ForEach(assembly =>
            AddAssemblyBusinessServiceImplementations(services, assembly)
        );

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceImplementations(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        BusinessServiceAssist.GetBusinessServiceInterfaceCollection().ForEach(serviceType =>
        {
            var implType = assembly.ExportedTypes.FirstOrDefault(type =>
                type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)
            );

            if (implType is null) return;

            services.TryAddScoped(implType);
            services.TryAddSingleton(new ProxyGenerator());
            services.TryAddScoped(serviceType, provider =>
            {
                var target = provider.GetService(implType);
                var interceptors = DefaultInterceptorTypes
                    .ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor)
                    .ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();

                if (proxyGenerator == null) throw new Exception("provider.GetService<ProxyGenerator>() result is null");

                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                    serviceType,
                    target,
                    interceptors
                );

                return proxy;
            });
        });

        return services;
    }
}