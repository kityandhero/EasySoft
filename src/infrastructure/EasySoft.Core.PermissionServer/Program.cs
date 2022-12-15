using EasySoft.Core.PermissionServer;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        args.ToArray()
    )
    .EasyBuild();

app.Run();