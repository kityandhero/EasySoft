using EasySoft.Core.PermissionServer;

EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        args.ToArray()
    )
    .EasyBuild();

app.Run();