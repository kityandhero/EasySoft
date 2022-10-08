using EasySoft.Core.Data.Interceptors;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Data.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifier = "5c82db26-dbf0-448f-bbd8-621e3bde9a1d";

    public static IServiceCollection AddAdvanceUnitOfWorkInterceptor(
        this IServiceCollection services
    )
    {
        if (services.HasRegistered(UniqueIdentifier))
            return services;

        services.AddScoped<UnitOfWorkInterceptor>();
        services.AddScoped<UnitOfWorkAsyncInterceptor>();

        return services;
    }
}