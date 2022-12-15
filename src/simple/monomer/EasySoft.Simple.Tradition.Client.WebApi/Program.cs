using EasySoft.Simple.Tradition.Client.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ClientWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

app.Run();