using EasySoft.Core.Exceptionless.Assists;

namespace EasySoft.Core.Exceptionless.ExtensionMethods;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAdvanceExceptionless(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection.AddExceptionless(exceptionlessConfiguration =>
            {
                exceptionlessConfiguration.ServerUrl = ExceptionlessConfigureAssist
                    .GetExceptionlessServerUrl(configuration)
                    .Value;

                exceptionlessConfiguration.ApiKey = ExceptionlessConfigureAssist
                    .GetExceptionlessApiKey(configuration)
                    .Value;
            }
        );

        return serviceCollection;
    }

    /// <summary>
    /// 注入日志配置辅助类
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    internal static IServiceCollection AddExceptionlessConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new ExceptionlessConfigureAssist(configuration));

        return services;
    }
}