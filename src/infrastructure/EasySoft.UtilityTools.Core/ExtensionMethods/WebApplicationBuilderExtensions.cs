namespace EasySoft.UtilityTools.Core.ExtensionMethods;

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
}