using EasySoft.Core.Data.Configures;

namespace EasySoft.Simple.Tradition.ApplicationConfig;

public static class ApplicationAssist
{
    public static void Init()
    {
        AuxiliaryConfigure.PromptConfigFileInfo = true;

        ContextConfigure.EnableDetailedErrors = true;
        ContextConfigure.EnableSensitiveDataLogging = true;
        // ContextConfigure.AutoEnsureCreated = true;
        ContextConfigure.AutoMigrate = true;
        ContextConfigure.AddEntityConfigureAssembly(typeof(Blog).Assembly);

        BusinessServiceConfigure.AddBusinessServiceInterfaceAssembly(typeof(IBlogService).Assembly);
        BusinessServiceConfigure.AddBusinessServiceImplementationAssembly(typeof(BlogService).Assembly);

        // 配置额外的构建项目
        ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddApiVersioning")
                .SetAction(applicationBuilder =>
                {
                    applicationBuilder.Services.AddApiVersioning(o =>
                    {
                        //return versions in a response header
                        o.ReportApiVersions = true;
                        //default version select 
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        //if not specifying an api version,show the default version
                        o.AssumeDefaultVersionWhenUnspecified = true;
                    });
                }),
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceDbContext<DataContext>")
                .SetAction(applicationBuilder =>
                {
                    //使用 Sql Server
                    applicationBuilder.AddAdvanceSqlServer<SqlServerDataContext>(
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
                }),
            // 自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
            // applicationBuilder => { applicationBuilder.AddStaticFileOptionsInjection<CustomStaticFileOptions>(); },
            new ExtraAction<WebApplicationBuilder>()
                .SetName("AddAdvanceJsonWebToken")
                .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); })
        );
    }
}