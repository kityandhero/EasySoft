namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifier = "e85c3371-e050-4974-b4ec-007325517d32";

    public static IServiceCollection AddAdvanceSqlServer<TContext, TEntityConfigure>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    ) where TContext : SqlServerContext where TEntityConfigure : class, IEntityConfigure
    {
        if (services.HasRegistered(UniqueIdentifier))
            return services;

        services.AddAdvanceContext<TContext>(optionsBuilder);

        services.AddAdvanceUnitOfWorkInterceptor();
        services.TryAddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddSingleton<IEntityConfigure, TEntityConfigure>();

        return services;
    }
}