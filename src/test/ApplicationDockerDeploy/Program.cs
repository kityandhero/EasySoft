using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        args.ToArray()
    )
    .EasyBuild();

app.MapGet("/", () => "Hello World!");

app.Run();