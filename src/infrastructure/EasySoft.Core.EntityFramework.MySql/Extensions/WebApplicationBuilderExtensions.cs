namespace EasySoft.Core.EntityFramework.MySql.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifier = "b1c4d305-2488-4271-bc75-4b9d27d1eed8";

    /// <summary>
    ///     注册基于 SqlServer 并已集成好数据操作上下文、工作单元以及通用仓储的 EntityFramework Core 工具集
    /// </summary>  
    /// <param name="builder"></param>
    /// <param name="connectionString"></param>
    /// <param name="action"></param>
    /// <returns></returns> 
    public static WebApplicationBuilder AddAdvanceMySql<TContext, TEntityConfigure>(
        this WebApplicationBuilder builder,
        string connectionString,
        Action<DbContextOptionsBuilder> action
    ) where TContext : MySqlContext where TEntityConfigure : class, IEntityConfigure
    {
        if (builder.HasRegistered(UniqueIdentifier))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMySql)}<{typeof(TContext).Name}>."
        );

        builder.Services.AddAdvanceMySql<TContext, TEntityConfigure>(opt =>
        {
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            action(opt);
        });

        return builder;
    }
}