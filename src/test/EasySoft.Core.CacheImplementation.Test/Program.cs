using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.CacheImplementation.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddAdvanceCacheImplementation();

var app = builder.Build();

// 宿主启动完成后触发，此时后台泵已就绪。
// app.Lifetime.ApplicationStarted.Register(() =>
//     {
//         _ = Task.Run(async () =>
//             {
//                 var cacheOperator = app.Services.GetRequiredService<IAsyncCacheOperator>();
//
//                 cacheOperator.Set(
//                     "a",
//                     1,
//                     TimeSpan.FromSeconds(1)
//                 );
//
//                 var a = cacheOperator.Get<int>("a");
//
//                 Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] -------------a:{a.Data}-------------");
//
//                 await Task.Delay(TimeSpan.FromSeconds(2));
//
//                 a = cacheOperator.Get<int>("a");
//
//                 Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] -------------a:{a.Data}-------------");
//             }
//         );
//     }
// );

var cacheOperator = app.Services.GetRequiredService<IAsyncCacheOperator>();

cacheOperator.Set(
    "a",
    1,
    TimeSpan.FromSeconds(1000)
);

var a = cacheOperator.Get<int>("a");

Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] -------------a:{a.Data}-------------");

await Task.Delay(TimeSpan.FromSeconds(2));

a = cacheOperator.Get<int>("a");

Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] -------------a:{a.Data}-------------");

app.Run();