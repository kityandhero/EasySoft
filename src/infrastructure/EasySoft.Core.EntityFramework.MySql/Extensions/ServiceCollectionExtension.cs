using EasySoft.Core.Data.ExtensionMethods;
using EasySoft.Core.Data.Repositories;
using EasySoft.Core.Data.Transactions;
using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.ExtensionMethods;
using EasySoft.Core.EntityFramework.Repositories;

namespace EasySoft.Core.EntityFramework.MySql.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueServiceIdentifier = "b188e673-14d1-41ed-8be7-d7a3a399e74f";

    public static IServiceCollection AddAdvanceMySql<TContext, TEntityConfigure>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    ) where TContext : MySqlContext where TEntityConfigure : class, IEntityConfigure
    {
        if (services.HasRegistered(UniqueServiceIdentifier))
            return services;

        services.AddAdvanceContext<TContext>(optionsBuilder);

        services.AddAdvanceUnitOfWorkInterceptor();
        services.TryAddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.TryAddSingleton<IEntityConfigure, TEntityConfigure>();

        return services;
    }
}