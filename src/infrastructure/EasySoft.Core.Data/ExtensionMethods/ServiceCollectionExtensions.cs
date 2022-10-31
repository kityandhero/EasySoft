using System.Reflection;
using EasySoft.Core.Data.Interceptors;
using EasySoft.Core.Infrastructure.Services;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.Data.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    private static List<Type> DefaultInterceptorTypes => new() { typeof(UnitOfWorkInterceptor) };

    private const string AdvanceUnitOfWorkInterceptorUniqueIdentifier = "5c82db26-dbf0-448f-bbd8-621e3bde9a1d";

    public static IServiceCollection AddAdvanceUnitOfWorkInterceptor(
        this IServiceCollection services
    )
    {
        if (services.HasRegistered(AdvanceUnitOfWorkInterceptorUniqueIdentifier))
            return services;

        services.AddScoped<UnitOfWorkInterceptor>();
        services.AddScoped<UnitOfWorkAsyncInterceptor>();

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceInterceptors(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        if (assemblies.Length <= 0) return services;

        assemblies.ForEach(assembly =>
            AddAssemblyBusinessServiceInterceptors(services, assembly)
        );

        return services;
    }

    public static IServiceCollection AddAssemblyBusinessServiceInterceptors(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var targetType = typeof(IBusinessService);

        var serviceTypes = assembly.GetExportedTypes()
            .Where(type => type.IsInterface && type.IsAssignableTo(targetType))
            .ToList();

        serviceTypes.ForEach(serviceType =>
        {
            var implType = assembly.ExportedTypes.FirstOrDefault(type =>
                type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)
            );

            if (implType is null) return;

            services.AddScoped(implType);
            services.TryAddSingleton(new ProxyGenerator());
            services.AddScoped(serviceType, provider =>
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