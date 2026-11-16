using EasySoft.Core.PrepareStartWork.PrepareWorks;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.PrepareStartWork.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入定制的启动预处理任务，注入任务将在应用启动时自动执行
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TPrepareStartWork"></typeparam>
    /// <returns></returns>
    private static WebApplicationBuilder AddPrepareStartWorkInjection<TPrepareStartWork>(
        this WebApplicationBuilder builder
    ) where TPrepareStartWork : class, IPrepareStartWork
    {
        builder.Services.AddSingleton<IPrepareStartWork, TPrepareStartWork>();

        return builder;
    }
}