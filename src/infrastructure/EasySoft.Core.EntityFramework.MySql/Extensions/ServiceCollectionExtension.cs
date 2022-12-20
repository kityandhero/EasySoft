using EasySoft.Core.EntityFramework.Extensions;

namespace EasySoft.Core.EntityFramework.MySql.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueServiceIdentifier = "b188e673-14d1-41ed-8be7-d7a3a399e74f";

    public static IServiceCollection AddAdvanceMySql<TContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    ) where TContext : MySqlContext
    {
        if (services.HasRegistered(UniqueServiceIdentifier))
            return services;

        services.AddAdvanceContext<TContext>(optionsBuilder);

        services.TryAddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        return services;
    }
}