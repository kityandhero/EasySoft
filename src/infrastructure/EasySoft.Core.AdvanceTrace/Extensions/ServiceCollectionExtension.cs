using EasySoft.Core.AdvanceTrace.Interceptors;
using EasySoft.Core.AdvanceTrace.Options;

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
        Action<TraceRecordTracePerformanceOptions> action = null
    )
    {
        services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<TraceRecordTracePerformanceOptions>>().Value;

                action?.Invoke(options);

                return new TraceRecordAsyncInterceptor(
                    trackingSourceName,
                    options
                );
            }
        );

        services.AddScoped<TraceRecordInterceptor>();

        return services;
    }
}