using EasySoft.Simple.Tradition.Common.Enums;
using EasySoft.Simple.Tradition.Management.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ManagementWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

app.Run();