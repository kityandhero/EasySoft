using EasySoft.Core.EntityFramework.Extensions;

namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    private const string UniqueIdentifier = "e85c3371-e050-4974-b4ec-007325517d32";

    /// <summary>
    /// AddAdvanceSqlServer
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsBuilder"></param>
    /// <typeparam name="TContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceSqlServer<TContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    ) where TContext : SqlServerContext
    {
        if (services.HasRegistered(UniqueIdentifier))
            return services;

        services.AddAdvanceContext<TContext>(optionsBuilder);

        services.TryAddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        return services;
    }
}