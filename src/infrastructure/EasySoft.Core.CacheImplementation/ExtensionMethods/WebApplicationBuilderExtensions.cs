namespace EasySoft.Core.CacheImplementation.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceCacheImplementation = "b73d0680-f482-4168-b752-6f33abb10c0f";

    /// <summary>
    /// 配置缓存模式
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceCacheImplementation(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceCacheImplementation))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceCacheImplementation)}."
        );

        builder.Services.AddAdvanceCacheImplementation(builder.Configuration);

        return builder;
    }
}