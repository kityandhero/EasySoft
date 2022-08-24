using Autofac;
using AutoFacTest.Implementations;
using AutoFacTest.Interfaces;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.JsonWebToken.ExtensionMethods;
using EasySoft.Core.PermissionVerification.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
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

var builder = WebApplicationBuilderAssist.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt =>
    {
        opt.UseSqlServer(
            DatabaseConfigAssist.GetMainConnection()
        );
    }
);

builder.UseAdvanceApplicationChannel(
    ApplicationChannelCollection.TestApplication.ToInt(),
    ApplicationChannelCollection.TestApplication.GetDescription()
);

builder.UsePrepareStartWorkInjection<SimplePrepareStartWork>();

//自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
// builder.UseStaticFileOptionsInjection<CustomStaticFileOptions>();

builder.UseAdvanceJsonWebToken<ApplicationOperator>();

// builder.UseEasyToken<CustomTokenSecretOptions, ApplicationOperator>();

// 自定义token密钥解析类
// builder.UseEasyToken<CustomTokenSecretOptions,CustomTokenSecret, ApplicationOperator>();

builder.UsePermissionVerification<ApplicationPermissionObserver>();

// builder.UseTokenSecretInjection<CustomTokenSecret>();

builder.UseExtraNormalInjection(containerBuilder =>
{
    containerBuilder.RegisterType<Simple>().As<ISimple>().SingleInstance();

    containerBuilder.RegisterType<AuthorRepository>().As<IAuthorRepository>().AsImplementedInterfaces();

    containerBuilder.RegisterType<BlogRepository>().As<IBlogRepository>().AsImplementedInterfaces();

    containerBuilder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency()
        .AsImplementedInterfaces();

    containerBuilder.RegisterType<BlogService>().As<IBlogService>().InstancePerDependency().AsImplementedInterfaces();
});

// SignalR
builder.Services.AddSignalR();

var app = builder.EasyBuild(new List<string> { "AreaTest", "AuthTest", "DataTest", "ComponentTest" });

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// // 可使用下列代码创建数据（删除数据库，更改数据模型，创建具有新架构的数据库），真实项目应当使用 Migrations 来做创建工作, Migrations 无法使用迁移更新 EnsureCreated 创建的数据库
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