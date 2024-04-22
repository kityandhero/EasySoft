using EasySoft.Simple.Tradition.Management.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ManagementWebApi,
        args.ToArray()
    )
    .EasyBuild();

app.Run();