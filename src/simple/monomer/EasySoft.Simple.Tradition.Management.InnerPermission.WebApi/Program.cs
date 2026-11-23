using EasySoft.Core.Web.Framework.Kernel.Assists;
using EasySoft.Simple.Tradition.Management.InnerPermission.WebApi;

var app = WebApplicationBuilderAssist
    .CreateBuilder<StartUpConfigure>(
        ApplicationChannelCollection.ManagementWebApi,
        args.ToArray()
    )
    .EasyBuild();

app.Run();