using Autofac;
using AutoFacTest.Implementations;
using AutoFacTest.Interfaces;
using EasySoft.Core.ActionMap.ExtensionMethods;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;
using EntityFrameworkTest.Repositories;
using EntityFrameworkTest.Services;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.EasyTokens;
using WebApplicationTest.Enums;
using WebApplicationTest.Hubs;
using WebApplicationTest.PrepareStartWorks;
using EasySoft.Core.EntityFramework.ExtensionMethods;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.LogDashboard.ExtensionMethods;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.PermissionVerification.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Web.Framework.ExtensionMethods;

// 配置额外的构建项目
ApplicationConfigActionAssist.AddWebApplicationBuilderExtraActions(
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceDbContext<DataContext>")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddAdvanceDbContext<DataContext>(
                opt =>
                {
                    opt.UseSqlServer(
                        DatabaseConfigAssist.GetMainConnection()
                    );
                }
            );
        }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceApplicationChannel")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddAdvanceApplicationChannel(
                ApplicationChannelCollection.TestApplication.ToInt(),
                ApplicationChannelCollection.TestApplication.GetDescription()
            );
        }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddPrepareStartWorkInjection")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddPrepareStartWorkInjection<SimplePrepareStartWork>();
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
        .SetName("AddExtraNormalInjection")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddExtraNormalInjection(containerBuilder =>
            {
                containerBuilder.RegisterType<Simple>().As<ISimple>().SingleInstance();

                containerBuilder.RegisterType<AuthorRepository>().As<IAuthorRepository>().AsImplementedInterfaces();

                containerBuilder.RegisterType<BlogRepository>().As<IBlogRepository>().AsImplementedInterfaces();

                containerBuilder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency()
                    .AsImplementedInterfaces();

                containerBuilder.RegisterType<BlogService>().As<IBlogService>().InstancePerDependency()
                    .AsImplementedInterfaces();
            });
        }),
    // 启用日志面板
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceLogDashboard")
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceLogDashboard(); }),
    // 配置健康检测
    // applicationBuilder =>
    // {
    //     applicationBuilder.AddAdvanceHealthChecks(new List<IAdvanceHealthCheck>
    //     {
    //         new HelloHealthCheck().ToIAdvanceHealthCheck()
    //     });
    // },
    // SignalR
    new ExtraAction<WebApplicationBuilder>().SetName("AddSignalR").SetAction(applicationBuilder =>
    {
        applicationBuilder.Services.AddSignalR();
    })
);

ApplicationConfigActionAssist.AddAreas("AreaTest", "AuthTest", "DataTest", "ComponentTest");

ApplicationConfigActionAssist.AddWebApplicationExtraAction(
    new ExtraAction<WebApplication>().SetName("UseActionMap")
        .SetAction(application => application.UseActionMap())
);

AgileConfigClientActionAssist.ActionAgileConfigChanged = e =>
{
    // LogAssist.Info("config changed");
};

var app = WebApplicationBuilderAssist
    .CreateBuilder(args.ToArray())
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

// SignalR
app.MapHub<ChatHub>("/chatHub");

app.Run();