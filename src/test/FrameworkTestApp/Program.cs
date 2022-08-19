using Autofac;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EntityFrameworkTest.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplicationBuilderAssist.CreateBuilder(
    args
);

builder.Services.AddDbContext<DataContext>(opt => { opt.UseSqlServer(DatabaseConfigAssist.GetMainConnection()); });

var app = builder.EasyBuild();

app.MapGet("/", () => "Hello World!");

app.Run();