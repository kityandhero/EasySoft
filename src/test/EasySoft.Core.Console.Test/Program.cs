using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using EasySoft.Core.Hosting.Framework.Kernel.Assists;
using EasySoft.UtilityTools.Core.Assists;

var hostBuilder = HostBuilderAssist.CreateBuilder(args);

var host = hostBuilder.Build();

host.StartAsync();