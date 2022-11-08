using System.Reflection;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Data.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要先行收集逻辑服务接口, 即调用 AddAssemblyBusinessServiceInterfaces
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceInterfaceAssemblies">服务定义程序集</param>
    /// <param name="serviceImplementationAssemblies">服务实现程序集</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAssemblyBusinessServices(
        this WebApplicationBuilder builder,
        IEnumerable<Assembly> serviceInterfaceAssemblies,
        IEnumerable<Assembly> serviceImplementationAssemblies
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServices)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterfaces(serviceInterfaceAssemblies.ToArray());
        builder.Services.AddAssemblyBusinessServiceImplementations(serviceImplementationAssemblies.ToArray());

        return builder;
    }

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要先行收集逻辑服务接口, 即调用 AddAssemblyBusinessServiceInterfaces
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceInterfaceAssembly">服务定义程序集</param>
    /// <param name="serviceImplementationAssembly">服务实现程序集</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAssemblyBusinessServices(
        this WebApplicationBuilder builder,
        Assembly serviceInterfaceAssembly,
        Assembly serviceImplementationAssembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServices)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterfaces(serviceInterfaceAssembly);
        builder.Services.AddAssemblyBusinessServiceImplementations(serviceImplementationAssembly);

        return builder;
    }

    /// <summary>
    /// 收集程序集中的逻辑服务接口
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceInterfaces(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceInterfaces)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterfaces(assemblies);

        return builder;
    }

    /// <summary>
    /// 收集程序集中的逻辑服务接口  
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assembly"></param> 
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceInterfaces(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceInterfaces)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterfaces(assembly);

        return builder;
    }

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要先行收集逻辑服务接口, 即调用 AddAssemblyBusinessServiceInterfaces
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceImplementations(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceImplementations)}()."
        );

        builder.Services.AddAssemblyBusinessServiceImplementations(assemblies);

        return builder;
    }

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要先行收集逻辑服务接口, 即调用 AddAssemblyBusinessServiceInterfaces
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assembly"></param> 
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceImplementations(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceImplementations)}()."
        );

        builder.Services.AddAssemblyBusinessServiceImplementations(assembly);

        return builder;
    }
}