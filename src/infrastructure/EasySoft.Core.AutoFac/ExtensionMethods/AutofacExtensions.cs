using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace EasySoft.Core.AutoFac.ExtensionMethods;

public static class AutofacExtensions
{
    /// <summary>
    /// 通过指定的程序集进行批量注册
    /// </summary>
    /// <typeparam name="T">注册实现了T类型的非虚类。如果T是接口，会同时注册到该接口类型中</typeparam>
    /// <param name="builder"></param>
    /// <param name="assemblies">待扫描的程序集</param>
    /// <param name="serviceNameMapping">可以指定注册的映射关系。将类和指定key关联，以便能根据key获取指定类实例</param>
    /// <returns></returns>
    public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
        RegisterAssemblyServices<T>(
            this ContainerBuilder builder,
            Assembly[] assemblies,
            Func<Type, string>? serviceNameMapping = null
        ) where T : class
    {
        if (assemblies == null || !assemblies.Any())
        {
            throw new ArgumentNullException(nameof(assemblies));
        }

        var registrationBuilder = builder.RegisterAssemblyTypes(assemblies)
            .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract)
            .AsSelf();

        var registerAssemblyTypes = builder.RegisterAssemblyTypes(assemblies);

        // foreach (var item in registerAssemblyTypes)
        // {
        //     
        // }

        if (typeof(T).IsInterface)
        {
            registrationBuilder = registrationBuilder.AsImplementedInterfaces();
        }

        if (serviceNameMapping != null)
        {
            registrationBuilder = registrationBuilder.Named<T>(serviceNameMapping);
        }

        return registrationBuilder;
    }
}