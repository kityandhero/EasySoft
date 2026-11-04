namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HttpClientBuilderExtension
/// </summary>
public static class HttpClientBuilderExtension
{
    /// <summary>
    /// 批量注入Polly策略，全部合并到单个ResiliencePipeline，避免多个独立DelegatingHandler
    /// </summary>
    public static IHttpClientBuilder AddPolicyHandlerICollection(
        this IHttpClientBuilder builder,
        List<IAsyncPolicy<HttpResponseMessage>> policies
    )
    {
        policies?.ForEach(policy => builder.AddPolicyHandler(policy));
        return builder;
    }
}