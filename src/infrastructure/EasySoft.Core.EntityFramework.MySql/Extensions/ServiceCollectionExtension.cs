namespace EasySoft.Core.EntityFramework.MySql.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueServiceIdentifier = "b188e673-14d1-41ed-8be7-d7a3a399e74f";

    public static IServiceCollection AddAdvanceEntityFrameworkMySql(this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    )
    {
        if (services.HasRegistered(UniqueServiceIdentifier))
            return services;

        services.TryAddScoped<IUnitOfWork, UnitOfWork<DataContext>>();
        // services.TryAddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
        // services.TryAddScoped(typeof(IEfBasicRepository<>), typeof(EfBasicRepository<>));
        services.AddDbContext<DbContext, DataContext>(optionsBuilder);

        return services;
    }
}