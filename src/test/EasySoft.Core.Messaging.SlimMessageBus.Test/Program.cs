using System.Diagnostics;
using System.Threading.Channels;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Abstractions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Collectors;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Extensions;
using EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

var builder = WebApplication.CreateBuilder(args);

#region RabbitMQ Build

// 1) 拦截器 + 观测器 + 公共门面。不含任何子总线配置。
// builder.Services.AddScaffoldMessagingCore();
// builder.Services.AddScaffoldMessagingContracts();

// 2) 各模块独立注册自己的子总线，互不感知。
// builder.Services.AddInfraBusRabbitMQCore(builder.Configuration);

// 发消息
// builder.Services.AddInfraRabbitMQProducer();

// 收消息
// builder.Services.AddInfraRabbitMQConsumer();

// builder.Services.AddOrderMessaging(builder.Configuration);

// 3) 序列化配置为共享设置，任一次 AddSlimMessageBus 中声明一次即可。
// builder.Services.AddSlimMessageBus(mbb => mbb.AddJsonSerializer());

#endregion

#region ImMemory Build

builder.Services.AddInformationInMemory(opt =>
    {
        opt.Capacity = 500; // 队列上限 500 条

        // opt.FullMode = BoundedChannelFullMode.DropWrite;

        // 队列满时，生产者等待
        // opt.FullMode = BoundedChannelFullMode.Wait;

        // 队列满时，抛弃陈旧数据
        opt.FullMode = BoundedChannelFullMode.DropOldest;
    }
);

#endregion

var app = builder.Build();

#region RabbitMQ Collector

// 4) 宿主已启动，所有子总线通道就绪，入口直接调用。
// 解析 RabbitMQRabbitMQInformationCollector 触发 Keyed 工厂委托，返回包装根容器 IMessageBus 的适配器。
// 发布 RabbitMQInformationCollectedEvent 时，复合总线自动路由到 "infra" 子总线。
// var collector = app.Services.GetRequiredService<IRabbitMQInformationCollector>();

// await collector.CollectAsync(
//     new RabbitMQInformationCollectedEvent(
//         "Startup",
//         "audit",
//         "host started",
//         DateTimeOffset.UtcNow
//     )
// );

#endregion

#region InMemory Collector

// 宿主启动完成后触发，此时后台泵已就绪。
app.Lifetime.ApplicationStarted.Register(() =>
    {
        _ = Task.Run(async () =>
            {
                var concrete = app.Services.GetRequiredService<InMemoryInformationCollector>();
                var sw = Stopwatch.StartNew();

                // 非阻塞突发发送 2000 条。TryWrite 恒为纳秒级，整个循环应远小于 100ms。
                for (var i = 0; i < 2000; i++)
                {
                    await concrete.CollectAsync(
                        new InMemoryInformationCollectedEvent(
                            "BurstTest",
                            "test",
                            $"msg-{i}",
                            DateTimeOffset.UtcNow
                        )
                    );
                }

                sw.Stop();

                Console.WriteLine($"Fired 2000 sends in {sw.ElapsedMilliseconds}ms");
                Console.WriteLine($"Enqueued: {concrete.EnqueuedCount}");
                Console.WriteLine($"Dropped:  {concrete.DroppedCount}");

                // 给后台泵 2 秒消化队列。
                await Task.Delay(TimeSpan.FromSeconds(2));

                Console.WriteLine($"After 2s — Enqueued: {concrete.EnqueuedCount}, Dropped: {concrete.DroppedCount}");
            }
        );
    }
);

#endregion

app.Run();