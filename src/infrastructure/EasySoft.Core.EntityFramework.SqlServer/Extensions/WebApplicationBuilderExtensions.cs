using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifier = "6909f3a5-aa12-4cb4-8637-c412f4beb9af";

    /// <summary>
    ///     已集成工作单元以及拦截器的 EntityFramework Core SqlServer
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="connectionString"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceEntityFrameworkSqlServer<T>(
        this WebApplicationBuilder builder,
        string connectionString,
        Action<DbContextOptionsBuilder> action
    ) where T : BaseContext
    {
        if (builder.HasRegistered(UniqueIdentifier))
            return builder;

        builder.Services.AddAdvanceEntityFrameworkSqlServer<T>(opt =>
        {
            opt.UseSqlServer(connectionString);

            action(opt);
        });

        return builder;
    }
}