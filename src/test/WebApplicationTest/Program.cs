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

var app = builder.EasyBuild(new List<string> { "AreaTest" });

app.MapGet("/", () => "Hello World!");

app.Run();