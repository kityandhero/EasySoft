using EasySoft.Core.AdvanceTrace.Interceptors;
using EasySoft.Core.AdvanceTrace.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EasySoft.Core.AdvanceTrace.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入轨迹跟踪
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="trackingSourceName">追踪源名称</param>
    /// <param name="action">选项配置器</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddAdvanceTrace(
        this IServiceCollection services,
        string trackingSourceName,
        Action<LogRecordTracePerformanceOptions> action = null
    )
    {
        services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<LogRecordTracePerformanceOptions>>().Value;

                action?.Invoke(options);

                return new LogRecordAsyncInterceptor(
                    trackingSourceName,
                    options
                );
            }
        );

        services.AddScoped<LogRecordInterceptor>();

        return services;
    }
}