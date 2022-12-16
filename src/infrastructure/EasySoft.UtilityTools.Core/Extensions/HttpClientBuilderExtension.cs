namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HttpClientBuilderExtension
/// </summary>
public static class HttpClientBuilderExtension
{
    /// <summary>
    /// AddPolicyHandlerICollection
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="policies"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddPolicyHandlerICollection(this IHttpClientBuilder builder,
        List<IAsyncPolicy<HttpResponseMessage>> policies)
    {
        policies?.ForEach(policy => builder.AddPolicyHandler(policy));

        return builder;
    }
}