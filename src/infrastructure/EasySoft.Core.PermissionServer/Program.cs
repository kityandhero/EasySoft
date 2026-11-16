using EasySoft.Core.PermissionServer;
using EasySoft.Core.Web.Framework.Kernel.Assists;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        args.ToArray()
    )
    .EasyBuild();

app.Run();