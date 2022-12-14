using EasySoft.Core.Refit.Assists;

namespace EasySoft.Core.Refit.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// AddAdvanceRefitClient
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <typeparam name="TAdvanceRefitClient"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceRefitClient<TAdvanceRefitClient>(
        this IServiceCollection services,
        Action<IHttpClientBuilder>? action = null
    ) where TAdvanceRefitClient : class
    {
        var policies = RefitAssist.GenerateRefitPolicies();

        //注册RefitClient,设置httpclient生命周期时间，默认也是2分钟。
        var contentSerializer = new NewtonsoftJsonContentSerializer(JsonConvertAssist.CreateJsonSerializerSettings());
        var refitSettings = new RefitSettings(contentSerializer);

        var clientBuilder = services.AddRefitClient<TAdvanceRefitClient>(refitSettings)
            .SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .AddPolicyHandlerICollection(policies);

        action?.Invoke(clientBuilder);

        return services;
    }
}