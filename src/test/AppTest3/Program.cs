using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EntityFrameworkTest.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplicationBuilderAssist.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString(DatabaseConfigAssist.GetMainConnection()));
});

var app = WebApplicationBuilderExtensions.EasyBuild(builder);

app.MapGet("/", () => "Hello World!");

app.Run();