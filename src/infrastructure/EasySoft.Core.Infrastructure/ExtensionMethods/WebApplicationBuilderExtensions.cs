using System.Reflection;
using EasySoft.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddServicesWithInterceptors(
        this WebApplicationBuilder builder,
        HashSet<Type> hashSet,
        params Assembly[] assemblies
    )
    {
        // if (hashSet.Count <= 0) return builder;

        if (assemblies.Length <= 0) return builder;

        var appServiceType = typeof(IService);

        var serviceTypes = assemblies.SelectMany(o =>
        {
            return o.GetExportedTypes()
                .Where(type => type.IsInterface && type.IsAssignableTo(appServiceType))
                .ToList();
        }).ToList();

        serviceTypes.ForEach(serviceType =>
        {
            var implType = ApplicationLayerAssembly.ExportedTypes.FirstOrDefault(type =>
                type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true));
            if (implType is null)
                return;

            Services.AddScoped(implType);
            Services.TryAddSingleton(new ProxyGenerator());
            Services.AddScoped(serviceType, provider =>
            {
                var interfaceToProxy = serviceType;
                var target = provider.GetService(implType);
                var interceptors = DefaultInterceptorTypes
                    .ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor).ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();
                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(interfaceToProxy, target,
                    interceptors);
                return proxy;
            });
        });

        return builder;
    }
}