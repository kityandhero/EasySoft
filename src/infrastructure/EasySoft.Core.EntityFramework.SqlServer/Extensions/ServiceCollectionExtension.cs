using EasySoft.Core.Data.ExtensionMethods;
using EasySoft.Core.Data.Repositories;
using EasySoft.Core.Data.Transactions;
using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.ExtensionMethods;
using EasySoft.Core.EntityFramework.Repositories;
using EasySoft.Core.EntityFramework.SqlServer.Transactions;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

public static class ServiceCollectionExtension
{
    private const string UniqueIdentifier = "e85c3371-e050-4974-b4ec-007325517d32";

    public static IServiceCollection AddAdvanceEntityFrameworkSqlServer<TContext, TEntityConfigure>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    ) where TContext : BaseContext where TEntityConfigure : class, IEntityConfigure
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