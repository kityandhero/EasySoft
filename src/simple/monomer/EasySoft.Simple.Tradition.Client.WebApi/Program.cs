using EasySoft.Simple.Tradition.Client.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ClientWebApi,
        args.ToArray()
    )
    .EasyBuild();

app.Run();