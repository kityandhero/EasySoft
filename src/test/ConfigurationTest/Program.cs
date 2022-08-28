// See https://aka.ms/new-console-template for more information

using EasySoft.Configuration.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

Console.WriteLine("Hello, World!");

var o = new ConfigurationBuilder().AddJsonContent(
    JsonConvertAssist.Serialize(new { a = 1 }),
    out var source
).Build();

_ = ChangeToken.OnChange(() => o.GetReloadToken(), () => { Console.WriteLine("changed"); });

Thread.Sleep(1000);

source.SetJsonContent(JsonConvertAssist.Serialize(new { a = 2 }));

Thread.Sleep(1000);

source.SetJsonContent(JsonConvertAssist.Serialize(new { a = 3 }));

Thread.Sleep(1000);