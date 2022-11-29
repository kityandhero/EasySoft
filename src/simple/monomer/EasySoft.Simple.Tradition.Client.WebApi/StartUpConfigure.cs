using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Simple.Tradition.Client.WebApi.EventSubscribers;
using EasySoft.Simple.Tradition.Client.WebApi.Security;
using EasySoft.Simple.Tradition.Data.Contexts;
using EasySoft.Simple.Tradition.Data.EntityConfigures;
using EasySoft.Simple.Tradition.Service.Services.Implementations;

namespace EasySoft.Simple.Tradition.Client.WebApi;

public class StartUpConfigure : IStartUpConfigure
{
    public void Init()
    {
        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceDbContext<DataContext>")
                .SetAction(applicationBuilder =>
                {
                    //使用 Sql Server
                    applicationBuilder.AddAdvanceSqlServer<SqlServerDataContext, IntegrationEntityConfigure>(
                        DatabaseConfigAssist.GetMainConnection(),
                        opt =>
                        {
                            //自动转换命名格式
                            opt.UseSnakeCaseNamingConvention();
                        }
                    );

                    // 使用 MySql
                    // applicationBuilder.AddAdvanceMySql<MySqlDataContext, IntegrationEntityConfigure>(
                    //     DatabaseConfigAssist.GetMainConnection(),
                    //     opt =>
                    //     {
                    //         //自动转换命名格式
                    //         opt.UseSnakeCaseNamingConvention();
                    //     }
                    // );

                    applicationBuilder.AddAssemblyBusinessServices(
                        typeof(IBlogService).Assembly,
                        typeof(BlogService).Assembly
                    );
                }),
            // 自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
            // applicationBuilder => { applicationBuilder.AddStaticFileOptionsInjection<CustomStaticFileOptions>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceJsonWebToken")
                .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); }),
            // applicationBuilder => { applicationBuilder.UseEasyToken<CustomTokenSecretOptions, ApplicationOperator>(); },
            // 自定义token密钥解析类
            // applicationBuilder => { applicationBuilder.UseEasyToken<CustomTokenSecretOptions, CustomTokenSecret, ApplicationOperator>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddPermissionVerification")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
                }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddCapEventSubscriber")
                .SetAction(applicationBuilder => { applicationBuilder.AddCapEventSubscriber<CapEventSubscriber>(); })
        );
    }
}