using EasySoft.Core.Data.Transactions;
using EasySoft.Core.EntityFramework.SqlServer.Transactions;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAdvanceEntityFrameworkSqlServer(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsBuilder
    )
    {
        if (services.HasRegistered(nameof(AddAdvanceEntityFrameworkSqlServer)))
            return services;

        services.TryAddScoped<IUnitOfWork, UnitOfWork<DataContext>>();
        // services.TryAddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
        // services.TryAddScoped(typeof(IEfBasicRepository<>), typeof(EfBasicRepository<>));
        services.AddDbContext<DbContext, DataContext>(optionsBuilder);

        return services;
    }
}