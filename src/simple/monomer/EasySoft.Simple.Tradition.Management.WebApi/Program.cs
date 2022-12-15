using EasySoft.Simple.Tradition.Management.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ManagementWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

app.Run();