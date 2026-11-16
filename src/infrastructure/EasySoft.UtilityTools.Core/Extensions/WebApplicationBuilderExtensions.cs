namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    /// <summary>
    /// HasRegistered
    /// </summary>
    /// <param name="_"></param>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public static bool HasRegistered(this WebApplicationBuilder _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }

    /// <summary>
    /// AddLogRecordInterceptor
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddLogRecordInterceptor(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogRecordInterceptor();

        return builder;
    }

    /// <summary>
    /// AddExtraNormalInjection
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static WebApplicationBuilder AddExtraNormalInjection(
        this WebApplicationBuilder builder,
        Action<IServiceCollection> action
    )
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        builder.Host.ConfigureServices(action);

        return builder;
    }
}