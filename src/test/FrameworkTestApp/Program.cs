using Autofac;
using EntityFrameworkTest.Contexts;
using EasySoft.Core.Mvc.Framework.BuilderAssists;
using EasySoft.Core.Mvc.Framework.ConfigAssist;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplicationBuilderAssist.CreateBuilder(
    args
);

builder.Services.AddDbContext<DataContext>(opt => { opt.UseSqlServer(DatabaseConfigAssist.GetMainConnection()); });

var app = builder.EasyBuild();

app.MapGet("/", () => "Hello World!");

app.Run();