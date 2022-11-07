using System.Reflection;
using Asp.Versioning;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Data.ExtensionMethods;
using EasySoft.Core.EntityFramework.MySql.Extensions;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.LogDashboard.ExtensionMethods;
using EasySoft.Core.MediatR.ExtensionMethods;
using EasySoft.Core.PermissionVerification.ExtensionMethods;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Simple.AccountCenter.Application.Contracts.Services;
using EasySoft.Simple.AccountCenter.Application.Services;
using EasySoft.Simple.AccountCenter.Domain.EntityConfigures;
using EasySoft.Simple.DomainDrivenDesign.Domain.Shared.Enums;
using EasySoft.Simple.DomainDrivenDesign.Infrastructure.Authentication;
using EasySoft.Simple.DomainDrivenDesign.Infrastructure.Contexts;
using EasySoft.Simple.DomainDrivenDesign.Infrastructure.Permissions;
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
            applicationBuilder.AddAdvanceMySql<DataContext, IntegrationEntityConfigure>(
                DatabaseConfigAssist.GetMainConnection(),
                opt =>
                {
                    //自动转换命名格式
                    opt.UseSnakeCaseNamingConvention();
                }
            );

            applicationBuilder.AddAssemblyBusinessServices(
                typeof(IUserService).Assembly,
                typeof(UserService).Assembly
            );
        }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceJsonWebToken")
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddPermissionVerification")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
        }),
    // 启用日志面板
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceLogDashboard")
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceLogDashboard(); }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddPermissionVerification")
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceMediatR(Assembly.GetExecutingAssembly()); })
    // 配置健康检测
    // applicationBuilder =>
    // {
    //     applicationBuilder.AddAdvanceHealthChecks(new List<IAdvanceHealthCheck>
    //     {
    //         new HelloHealthCheck().ToIAdvanceHealthCheck()
    //     });
    // },
);

AgileConfigClientActionAssist.ActionAgileConfigChanged = _ =>
{
    // LogAssist.Info("config changed");
};

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        ApplicationChannelCollection.AccountCenter.ToApplicationChannel(),
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