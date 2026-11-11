using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.Core.Infrastructure.Extensions;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddJsonSerializerSettings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJsonSerializerSettings(
        this IServiceCollection services
    )
    {
        services.AddSingleton(JsonConvertAssist.CreateJsonSerializerSettings(false));

        return services;
    }
}