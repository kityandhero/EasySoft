using EasySoft.Core.Web.Framework.BuilderAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Simple.Distribute.Common.Enums;

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        ApplicationChannelCollection.GateWayOcelot.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

// 可使用下列代码创建数据（删除数据库，更改数据模型，创建具有新架构的数据库），真实项目应当使用 Migrations 来做创建工作, Migrations 无法使用迁移更新 EnsureCreated 创建的数据库
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     var context = services.GetRequiredService<DataContext>();
//
//     context.Database.EnsureCreated();
// }

app.MapGet("/", () => "Hello World!");

app.Run();