using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Core.Web.Framework.Kernel.Assists;

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        args.ToArray()
    )
    .EasyBuild();

app.MapGet("/", () => "Hello World!");

app.Run();