namespace EasySoft.Core.Refit.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// AddAdvanceSwagger
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceRefitClient<TAdvanceRefitClient>(
        this WebApplicationBuilder builder,
        Action<IHttpClientBuilder>? action = null
    ) where TAdvanceRefitClient : class
    {
        builder.Services.AddAdvanceRefitClient<TAdvanceRefitClient>(action);

        return builder;
    }
}