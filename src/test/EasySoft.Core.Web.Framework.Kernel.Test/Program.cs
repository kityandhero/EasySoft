using EasySoft.Core.Web.Framework.Kernel.Assists;
using EasySoft.Core.Web.Framework.Kernel.ExtensionMethods.WebApplicationBuilderExtensions;

var builder = WebApplicationBuilderAssist.CreateBuilder(args.ToArray());

var app = builder.BuildWebApplication();

app.Run();