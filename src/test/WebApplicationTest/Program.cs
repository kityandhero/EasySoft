using Autofac;
using AutoFacTest.Implementations;
using AutoFacTest.Interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;
using EntityFrameworkTest.Repositories;
using EntityFrameworkTest.Services;
using EasySoft.Core.Mvc.Framework.BuilderAssists;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
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

builder.AddPrepareStartWorkInjection<SimplePrepareStartWork>();

builder.AddExtraNormalInjection(containerBuilder =>
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

var app = builder.EasyBuild(new List<string> { "AreaTest" });

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