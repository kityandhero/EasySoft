using EasySoft.Core.Data.Interceptors;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Data.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAdvanceUnitOfWorkInterceptor(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    )
    {
        if (services.HasRegistered(nameof(AddAdvanceUnitOfWorkInterceptor)))
            return services;

        services.AddScoped<UnitOfWorkInterceptor>();
        services.AddScoped<UnitOfWorkAsyncInterceptor>();

        return services;
    }
}