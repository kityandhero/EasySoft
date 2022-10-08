namespace EasySoft.Core.EntityFramework.MySql.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAdvanceEntityFrameworkMySql(this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    )
    {
        if (services.HasRegistered(nameof(AddAdvanceEntityFrameworkMySql)))
            return services;

        services.TryAddScoped<IUnitOfWork, UnitOfWork<DataContext>>();
        // services.TryAddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
        // services.TryAddScoped(typeof(IEfBasicRepository<>), typeof(EfBasicRepository<>));
        services.AddDbContext<DbContext, DataContext>(optionsBuilder);

        return services;
    }
}