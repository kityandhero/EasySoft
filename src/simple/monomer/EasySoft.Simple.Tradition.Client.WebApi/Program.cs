using EasySoft.Simple.Tradition.Client.WebApi;
using EasySoft.Simple.Tradition.Common.Enums;

EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ClientWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

app.Run();