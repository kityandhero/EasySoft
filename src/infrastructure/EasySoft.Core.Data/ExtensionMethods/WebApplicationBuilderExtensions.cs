using EasySoft.Core.Data.Configures;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Data.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAssemblyBusinessServices = "2e3d81ce-83d7-4dc6-810d-e6ea0d32b7fb";

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要配置 BusinessServiceConfigure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAssemblyBusinessServices(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAssemblyBusinessServices))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServices)}."
        );

        builder.AddAssemblyBusinessServiceInterfaces();
        builder.AddAssemblyBusinessServiceImplementations();

        return builder;
    }

    /// <summary>
    /// 收集程序集中的逻辑服务接口  
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceInterfaces(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceInterfaces)}."
        );

        var assemblies = BusinessServiceConfigure.GetBusinessServiceInterfaceAssemblies().ToArray();

        if (!assemblies.Any())
            StartupDescriptionMessageAssist.AddWarning(
                $"{nameof(BusinessServiceConfigure)}.{nameof(BusinessServiceConfigure.GetBusinessServiceInterfaceAssemblies)} has none."
            );

        StartupDescriptionMessageAssist.AddHint(
            $"{typeof(BusinessServiceConfigure).FullName}.{nameof(BusinessServiceConfigure.GetBusinessServiceInterfaceAssemblies)} contain {(!BusinessServiceConfigure.GetBusinessServiceInterfaceAssemblies().Any() ? "none" : BusinessServiceConfigure.GetBusinessServiceInterfaceAssemblies().Select(o => o.GetName().Name).Join(","))}."
        );

        builder.Services.AddAssemblyBusinessServiceInterfaces(
            assemblies
        );

        return builder;
    }

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理, 需要先行收集逻辑服务接口, 即调用 AddAssemblyBusinessServiceInterfaces
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static WebApplicationBuilder AddAssemblyBusinessServiceImplementations(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAssemblyBusinessServiceImplementations)}."
        );

        var assemblies = BusinessServiceConfigure.GetBusinessServiceImplementationAssemblies().ToArray();

        if (!assemblies.Any())
            StartupDescriptionMessageAssist.AddWarning(
                $"{nameof(BusinessServiceConfigure)}.{nameof(BusinessServiceConfigure.GetBusinessServiceImplementationAssemblies)} has none."
            );

        StartupDescriptionMessageAssist.AddHint(
            $"{typeof(BusinessServiceConfigure).FullName}.{nameof(BusinessServiceConfigure.GetBusinessServiceImplementationAssemblies)} contain {(!BusinessServiceConfigure.GetBusinessServiceImplementationAssemblies().Any() ? "none" : BusinessServiceConfigure.GetBusinessServiceImplementationAssemblies().Select(o => o.GetName().Name).Join(","))}."
        );

        builder.Services.AddAssemblyBusinessServiceImplementations(
            assemblies
        );

        return builder;
    }
}