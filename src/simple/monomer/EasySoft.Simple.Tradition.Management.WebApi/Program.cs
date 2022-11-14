using Asp.Versioning;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Data.ExtensionMethods;
using EasySoft.Core.EntityFramework.SqlServer.Extensions;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Simple.Tradition.Common.Enums;
using EasySoft.Simple.Tradition.Data.Contexts;
using EasySoft.Simple.Tradition.Data.EntityConfigures;
using EasySoft.Simple.Tradition.Management.WebApi.Security;
using EasySoft.Simple.Tradition.Service.Services.Implementations;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

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
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); })
);

ApplicationConfigurator.AddAreas("AuthTest", "DataTest");

AgileConfigClientActionAssist.ActionAgileConfigChanged = _ =>
{
    // LogAssist.Info("config changed");
};

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        ApplicationChannelCollection.ManagementWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

// 可使用下列代码创建数据（删除数据库，更改数据模型，创建具有新架构的数据库），真实项目应当使用 Migrations 来做创建工作, Migrations 无法使用迁移更新 EnsureCreated 创建的数据库
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     var context = services.GetRequiredService<DataContext>();
//
//     context.Database.EnsureCreated();
// }

app.MapGet("/", () => "Hello World!");

app.Run();