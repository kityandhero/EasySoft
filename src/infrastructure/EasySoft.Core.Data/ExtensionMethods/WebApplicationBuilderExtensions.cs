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

        var assemblies = BusinessServiceConfigure.BusinessServiceInterfaceAssemblies.ToArray();

        if (!assemblies.Any())
            StartupDescriptionMessageAssist.AddWarning(
                $"{nameof(BusinessServiceConfigure)}.{nameof(BusinessServiceConfigure.BusinessServiceInterfaceAssemblies)} has none."
            );

        StartupDescriptionMessageAssist.AddHint(
            $"{typeof(BusinessServiceConfigure).FullName}.{nameof(BusinessServiceConfigure.BusinessServiceInterfaceAssemblies)} contain {(!BusinessServiceConfigure.BusinessServiceInterfaceAssemblies.Any() ? "none" : BusinessServiceConfigure.BusinessServiceInterfaceAssemblies.Select(o => o.GetName().Name).Join(","))}."
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

        var assemblies = BusinessServiceConfigure.BusinessServiceImplementationAssemblies.ToArray();

        if (!assemblies.Any())
            StartupDescriptionMessageAssist.AddWarning(
                $"{nameof(BusinessServiceConfigure)}.{nameof(BusinessServiceConfigure.BusinessServiceImplementationAssemblies)} has none."
            );

        StartupDescriptionMessageAssist.AddHint(
            $"{typeof(BusinessServiceConfigure).FullName}.{nameof(BusinessServiceConfigure.BusinessServiceImplementationAssemblies)} contain {(!BusinessServiceConfigure.BusinessServiceImplementationAssemblies.Any() ? "none" : BusinessServiceConfigure.BusinessServiceImplementationAssemblies.Select(o => o.GetName().Name).Join(","))}."
        );

        builder.Services.AddAssemblyBusinessServiceImplementations(
            assemblies
        );

        return builder;
    }
}