using Autofac;
using AutoFacTest.Implementations;
using AutoFacTest.Interfaces;
using EntityFrameworkTest.Contexts;
using EntityFrameworkTest.IRepositories;
using EntityFrameworkTest.IServices;
using EntityFrameworkTest.Repositories;
using EntityFrameworkTest.Services;
using Framework.BuilderAssists;
using Framework.ConfigAssist;
using Framework.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplicationBuilderAssist.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt =>
    {
        opt.UseSqlServer(
            DatabaseConfigAssist.GetMainConnection()
        );
    }
);

builder.AddExtraNormalInjection(containerBuilder =>
{
    containerBuilder.RegisterType<Simple>().As<ISimple>().InstancePerDependency().SingleInstance();

    containerBuilder.RegisterType<AuthorRepository>().As<IAuthorRepository>().AsImplementedInterfaces();

    containerBuilder.RegisterType<BlogRepository>().As<IBlogRepository>().AsImplementedInterfaces();

    containerBuilder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency()
        .AsImplementedInterfaces();

    containerBuilder.RegisterType<BlogService>().As<IBlogService>().InstancePerDependency().AsImplementedInterfaces();
});

var app = builder.EasyBuild(new List<string> { "AreaTest" });

app.MapGet("/", () => "Hello World!");

app.Run();