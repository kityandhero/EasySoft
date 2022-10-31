using System.Reflection;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Data.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAssemblyBusinessServiceInterceptors(
        this WebApplicationBuilder builder,
        params Assembly[] assemblies
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAssemblyBusinessServiceInterceptors)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterceptors(assemblies);

        return builder;
    }

    /// <summary>
    /// 自动注册程序集中的逻辑服务以及工作单元拦截器代理  
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assembly"></param> 
    /// <returns></returns>
    public static WebApplicationBuilder AddAssemblyBusinessServiceInterceptors(
        this WebApplicationBuilder builder,
        Assembly assembly
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(AddAssemblyBusinessServiceInterceptors)}()."
        );

        builder.Services.AddAssemblyBusinessServiceInterceptors(assembly);

        return builder;
    }
}